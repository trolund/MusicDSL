module DSL.MusicBuilder


type MusicBuilder() =
    // Start with an empty composition
    member _.Yield _ = []

    // Combine two compositions (when adding multiple elements)
    member _.Combine(comp1, comp2) = comp1 @ comp2

    // Add a single element (like a note, chord, or rest)
    member _.Yield(element: MusicElement) = [ element ]

    // Handle an empty block
    member _.Zero() = []

    // Final result
    member _.Run(elements: MusicElement list) = elements

    // DSL methods as custom operations
    [<CustomOperation("note")>]
    member _.Note(elements: MusicElement list, pitch: string, duration: float) = elements @ [ Note(pitch, duration) ]

    [<CustomOperation("chord")>]
    member _.Chord(elements: MusicElement list, notes: string list, duration: float) =
        elements @ [ Chord(notes, duration) ]

    [<CustomOperation("rest")>]
    member _.Rest(elements: MusicElement list, duration: float) = elements @ [ Rest duration ]
