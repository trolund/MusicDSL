namespace DSL

type MusicElement =
    | Note of string * float // Note pitch (e.g., "C4") and duration in seconds
    | Chord of string list * float // List of notes in the chord and duration
    | Rest of float // Duration of silence

type Composition = MusicElement list // A sequence of music elements
