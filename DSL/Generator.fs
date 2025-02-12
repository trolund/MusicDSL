module DSL.Generator

open System
open System.IO

// Define basic audio parameters
let sampleRate = 44100 // Samples per second (CD quality)
let bitsPerSample = 16 // 16 bits per sample
let channels = 2 // stereo

// Frequencies for musical notes (in Hz)
let notesTones =
    dict [ "", 0.0
           "C4", 261.63
           "D4", 293.66
           "E4", 329.63
           "F4", 349.23
           "G4", 392.00
           "A4", 440.00
           "B4", 493.88
           "C5", 523.25
           "D5", 587.33
           "E5", 659.25
           "F5", 698.46
           "G5", 783.99
           "A5", 880.00
           "B5", 987.77
           "C6", 1046.50
           "Bb4", 466.16 ]

// Generate sine wave samples for a given frequency and duration
let generateSineWave (frequency: float) (duration: float) =
    let totalSamples = int (duration * float sampleRate)

    Array.init totalSamples (fun i ->
        let t = float i / float sampleRate // Time for each sample

        let sample =
            Math.Sin(2.0 * Math.PI * frequency * t)
            * float Int16.MaxValue

        int16 sample // Convert to 16-bit integer
    )
    
// Create a reverberation filter tacking int16 array and returning int16 array
let reverberationFilter (wave: int16 list) =
    let delay = 0.05
    let decay = 0.01

    let delaySamples = int (delay * float sampleRate)
    let decayFactor = float decay

    let rec applyReverberation (wave: int16 array) (delayedWave: int16 array) (index: int) =
        if index >= wave.Length then wave
        else            
            let delayedSample = delayedWave.[index % delayedWave.Length]
            let sample = wave.[index] + int16 (float delayedSample * decayFactor)
            
            wave.[index] <- sample
            delayedWave.[index % delayedWave.Length] <- sample

            applyReverberation wave delayedWave (index + 1)

    let res = applyReverberation (wave |> Array.ofList) (Array.zeroCreate delaySamples) 0
    
    (res |> List.ofArray)

let combineWaves (wave1: int16 list) (wave2: int16 list) =
    List.zip wave1 wave2
    |> List.map (fun (sample1, sample2) -> sample1 + sample2)

// Write a WAV file header
let writeWavHeader (writer: BinaryWriter) (dataSize: int) =
    writer.Write("RIFF".ToCharArray()) // ChunkID
    writer.Write(36 + dataSize) // ChunkSize
    writer.Write("WAVE".ToCharArray()) // Format
    writer.Write("fmt ".ToCharArray()) // Subchunk1ID
    writer.Write 16 // Subchunk1Size (PCM header)
    writer.Write(int16 1) // AudioFormat (PCM)
    writer.Write(int16 channels) // NumChannels
    writer.Write sampleRate // SampleRate
    writer.Write(sampleRate * channels * bitsPerSample / 8) // ByteRate
    writer.Write(int16 (channels * bitsPerSample / 8)) // BlockAlign
    writer.Write(int16 bitsPerSample) // BitsPerSample
    writer.Write("data".ToCharArray()) // Subchunk2ID
    writer.Write dataSize // Subchunk2Size

// Create a WAV file from a sequence of notes
let createWav fileName (noteSequence: Composition) =
    use stream = new FileStream(fileName, FileMode.Create)
    use writer = new BinaryWriter(stream)

    let combineWaves (waves: int16 array list) =
        List.fold (Array.map2 (fun a b -> a + b)) (Array.zeroCreate waves.Head.Length) waves

    // Generate all samples for the sequence
    let allSamples =
        noteSequence
        |> List.collect (fun elem ->
            match elem with
            | Note (note, duration) ->
                let frequency = notesTones[note]

                generateSineWave frequency duration
                |> Array.toList

            | Chord (notes, duration) ->
                let waves =
                    notes
                    |> List.map (fun note -> generateSineWave notesTones[note] duration)

                combineWaves waves |> Array.toList

            | Rest duration ->
                Array.init (int (duration * float sampleRate)) (fun _ -> int16 0)
                |> Array.toList)

    // Write the WAV file header
    let dataSize = allSamples.Length * 2 // 2 bytes per sample (16-bit audio)
    writeWavHeader writer dataSize

    // Write the audio samples
    allSamples
    |> List.iter (fun sample ->
        writer.Write(byte (sample &&& int16 0xFF)) // Low byte
        writer.Write(byte ((sample >>> 8) &&& int16 0xFF)) // High byte
    )

    printfn $"WAV file '%s{fileName}' created successfully!"
