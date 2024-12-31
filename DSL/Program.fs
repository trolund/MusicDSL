open DSL.Generator
open DSL.MusicBuilder

let music = MusicBuilder()

let song =
    music {
        // super mario bros theme
        note "E5" 0.5
        note "E5" 0.5
        rest 0.25
        note "E5" 0.5
        rest 0.25
        note "C5" 0.5
        note "E5" 0.5
        rest 0.25
        note "G5" 0.5
        rest 0.25
        note "G4" 0.5
        rest 0.25
        note "C5" 0.5
        rest 0.25
        note "G4" 0.5
        rest 0.25
        note "E4" 0.5
        rest 0.25
        note "A4" 0.5
        rest 0.25
        note "B4" 0.5
        rest 0.25
        note "Bb4" 0.5
        note "A4" 0.5
        rest 1.0
        note "G4" 0.5
        note "E5" 0.5
        note "G5" 0.5
        note "A5" 0.5
        rest 0.25
        note "F5" 0.5
        note "G5" 0.5
        rest 0.25
        note "E5" 0.5
        rest 0.25
        note "C5" 0.5
        note "D5" 0.5
        rest 0.25
        note "B4" 0.5
        rest 0.25
        note "C5" 0.5
        rest 0.25
        note "G4" 0.5
        rest 0.25
        note "E4" 0.5
        rest 0.25
        note "A4" 0.5
        rest 0.25
        note "B4" 0.5
        rest 0.25
        note "Bb4" 0.5
        note "A4" 0.5
        rest 1.0
    }

// Create the WAV file from the song
createWav "mario_bros_theme.wav" song
