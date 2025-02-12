# 🎵 F# Music DSL  

This project is an **internal DSL in F#** designed for simple and expressive music composition. It allows you to define melodies declaratively while handling the underlying sound generation.  

## ✨ Features  
- Define melodies using **note names and durations**  
- Generate **WAV files** from your compositions  
- Easily **play back** your music  
- Simple and expressive **DSL syntax**  

## 🚀 Quick Example  

Compose and play the **Super Mario Bros. theme song** with just a few lines of F#:  

```fsharp
open DSL.Generator
open DSL.MusicBuilder
open DSL.Player

let music = MusicBuilder()

let song =
    music {
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
        // ... more notes ...
    }

let songName = "mario_bros_theme.wav"
// Generate a WAV file
createWav songName song
// Play the generated song
playSound songName
```

## 🔥 Future Enhancements  
I'm planning to extend the DSL with:  
- 🎼 **Chords** for richer harmonies  
- 🔁 **Loops** for easier repetition  
- 🎹 **Instrument variations** for a more dynamic sound  

Contributions and feedback are welcome! 🎶🚀 
