namespace Koyomi.FSharp.Tests

open System

open Xunit
open FsUnit.Xunit

open Koyomi.FSharp

module EraTests =
    [<Fact>]
    let ``昭和は1926/12/25から始まる`` () =
        DateTime (1926, 12, 25)
        |> Era.from
        |> Era.nameAndYear
        |> should equal "昭和元年"

    [<Fact>]
    let ``昭和は1989/01/07に64年で終わる`` () =
        DateTime (1989, 1, 7)
        |> Era.from
        |> Era.nameAndYear
        |> should equal "昭和64年"

    [<Fact>]
    let ``平成は1989/01/08から始まる`` () =
        DateTime (1989, 1, 8)
        |> Era.from
        |> Era.nameAndYear
        |> should equal "平成元年"

    [<Fact>]
    let ``平成は2019/04/30に31年で終わる`` () =
        DateTime (2019, 4, 30)
        |> Era.from
        |> Era.nameAndYear
        |> should equal "平成31年"

    [<Fact>]
    let ``令和は2019/05/01から始まる`` () =
        DateTime (2019, 5, 1)
        |> Era.from
        |> Era.nameAndYear
        |> should equal "令和元年"

    [<Fact>]
    let ``昭和以前の日付は扱えない`` () =
        (fun () -> DateTime (1926, 12, 24) |> Era.from |> ignore)
        |> should throw typeof<System.ArgumentException>
