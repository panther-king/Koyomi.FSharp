[<AutoOpen>]
module Koyomi.FSharp.Tests.TestHelper

let isOkWith (expect: 'a) (actual: Result<'a, 'b>) =
    match actual with
    | Ok x when x = expect -> true
    | _ -> false
