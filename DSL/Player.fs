module DSL.Player

open System.Diagnostics
open System.IO

let playSound filePath =
    let workingDir = Directory.GetCurrentDirectory()
    let path = $"{workingDir}/{filePath}"
    let pInfo = ProcessStartInfo("ffplay", $"-nodisp -autoexit {path}")
    pInfo.UseShellExecute <- false
    let p= Process.Start(pInfo)
    p.WaitForExit()