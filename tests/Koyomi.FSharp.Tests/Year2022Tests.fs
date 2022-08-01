namespace Koyomi.FSharp.Tests

open System

open Xunit
open FsUnit.Xunit

open Koyomi.FSharp

module Year2022Tests =
    let january: obj[] list =
        [
            [| DateTime(2022, 1, 1); Some "元日"|]
            [| DateTime(2022, 1, 2); None |]
            [| DateTime(2022, 1, 3); None |]
            [| DateTime(2022, 1, 4); None |]
            [| DateTime(2022, 1, 5); None |]
            [| DateTime(2022, 1, 6); None |]
            [| DateTime(2022, 1, 7); None |]
            [| DateTime(2022, 1, 8); None |]
            [| DateTime(2022, 1, 9); None |]
            [| DateTime(2022, 1, 10); Some "成人の日" |]
            [| DateTime(2022, 1, 11); None |]
            [| DateTime(2022, 1, 12); None |]
            [| DateTime(2022, 1, 13); None |]
            [| DateTime(2022, 1, 14); None |]
            [| DateTime(2022, 1, 15); None |]
            [| DateTime(2022, 1, 16); None |]
            [| DateTime(2022, 1, 17); None |]
            [| DateTime(2022, 1, 18); None |]
            [| DateTime(2022, 1, 19); None |]
            [| DateTime(2022, 1, 20); None |]
            [| DateTime(2022, 1, 21); None |]
            [| DateTime(2022, 1, 22); None |]
            [| DateTime(2022, 1, 23); None |]
            [| DateTime(2022, 1, 24); None |]
            [| DateTime(2022, 1, 25); None |]
            [| DateTime(2022, 1, 26); None |]
            [| DateTime(2022, 1, 27); None |]
            [| DateTime(2022, 1, 28); None |]
            [| DateTime(2022, 1, 29); None |]
            [| DateTime(2022, 1, 30); None |]
            [| DateTime(2022, 1, 31); None |]
        ]

    [<Theory>]
    [<MemberData("january")>]
    let ``2022年1月の祝日を判定できる`` (dt: DateTime) (expect: string option) =
        Koyomi.from dt
        |> Koyomi.holiday
        |> should equal expect

    let february: obj[] list =
        [
            [| DateTime(2022, 2, 1); None |]
            [| DateTime(2022, 2, 2); None |]
            [| DateTime(2022, 2, 3); None |]
            [| DateTime(2022, 2, 4); None |]
            [| DateTime(2022, 2, 5); None |]
            [| DateTime(2002, 2, 6); None |]
            [| DateTime(2022, 2, 7); None |]
            [| DateTime(2022, 2, 8); None |]
            [| DateTime(2022, 2, 9); None |]
            [| DateTime(2022, 2, 10); None |]
            [| DateTime(2022, 2, 11); Some "建国記念の日" |]
            [| DateTime(2022, 2, 12); None |]
            [| DateTime(2022, 2, 13); None |]
            [| DateTime(2022, 2, 14); None |]
            [| DateTime(2022, 2, 15); None |]
            [| DateTime(2022, 2, 16); None |]
            [| DateTime(2022, 2, 17); None |]
            [| DateTime(2022, 2, 18); None |]
            [| DateTime(2022, 2, 19); None |]
            [| DateTime(2022, 2, 20); None |]
            [| DateTime(2022, 2, 21); None |]
            [| DateTime(2022, 2, 22); None |]
            [| DateTime(2022, 2, 23); Some "天皇誕生日" |]
            [| DateTime(2022, 2, 24); None |]
            [| DateTime(2022, 2, 25); None |]
            [| DateTime(2022, 2, 26); None |]
            [| DateTime(2022, 2, 27); None |]
            [| DateTime(2022, 2, 28); None |]
        ]

    [<Theory>]
    [<MemberData("february")>]
    let ``2022年2月の祝日を判定できる`` (dt: DateTime) (expect: string option) =
        Koyomi.from dt
        |> Koyomi.holiday
        |> should equal expect
