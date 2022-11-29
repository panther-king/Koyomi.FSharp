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
        |> should equal
            { Epoc = Showa
              FullName = "昭和元年"
              Name = "昭和"
              Year = 1 }

    [<Fact>]
    let ``昭和は1989/01/07に64年で終わる`` () =
        DateTime (1989, 1, 7)
        |> Era.from
        |> should equal
            { Epoc = Showa
              FullName = "昭和64年"
              Name = "昭和"
              Year = 64 }

    [<Fact>]
    let ``平成は1989/01/08から始まる`` () =
        DateTime (1989, 1, 8)
        |> Era.from
        |> should equal
            { Epoc = Heisei
              FullName = "平成元年"
              Name = "平成"
              Year = 1 }

    [<Fact>]
    let ``平成は2019/04/30に31年で終わる`` () =
        DateTime (2019, 4, 30)
        |> Era.from
        |> should equal
            { Epoc = Heisei
              FullName = "平成31年"
              Name = "平成"
              Year = 31 }

    [<Fact>]
    let ``令和は2019/05/01から始まる`` () =
        DateTime (2019, 5, 1)
        |> Era.from
        |> should equal
            { Epoc = Reiwa
              FullName = "令和元年"
              Name = "令和"
              Year = 1 }

    [<Fact>]
    let ``昭和以前の日付は扱えない`` () =
        (fun () -> DateTime (1926, 12, 24) |> Era.from |> ignore)
        |> should throw typeof<System.ArgumentException>
