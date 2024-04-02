namespace Koyomi.FSharp.Tests

open System

open Xunit
open FsUnit.Xunit

open Koyomi.FSharp

module EraTests =
    let meiji: obj[] list =
        [ [| 1868; 10; 23; Meiji(1) |]; [| 1912; 7; 29; Meiji(45) |] ]

    [<Theory>]
    [<MemberData("meiji")>]
    let ``明治は1868年10月23日から1912年7月29日までである`` (y: int) (m: int) (d: int) (expect: JapaneseEra) =
        DateOnly(y, m, d)
        |> JapaneseEra.fromDateOnly
        |> isSomeOf expect
        |> should be True

    let taisho: obj[] list =
        [ [| 1912; 7; 30; Taisho(1) |]; [| 1926; 12; 24; Taisho(15) |] ]

    [<Theory>]
    [<MemberData("taisho")>]
    let ``大正は1912年7月30日から1926年12月24日までである`` (y: int) (m: int) (d: int) (expect: JapaneseEra) =
        DateOnly(y, m, d)
        |> JapaneseEra.fromDateOnly
        |> isSomeOf expect
        |> should be True

    let showa: obj[] list =
        [ [| 1926; 12; 25; Showa(1) |]; [| 1989; 1; 7; Showa(64) |] ]

    [<Theory>]
    [<MemberData("showa")>]
    let ``昭和は1926年12月25日から1989年1月7日までである`` (y: int) (m: int) (d: int) (expect: JapaneseEra) =
        DateOnly(y, m, d)
        |> JapaneseEra.fromDateOnly
        |> isSomeOf expect
        |> should be True

    let heisei: obj[] list =
        [ [| 1989; 1; 8; Heisei(1) |]; [| 2019; 4; 30; Heisei(31) |] ]

    [<Theory>]
    [<MemberData("heisei")>]
    let ``平成は1989年1月8日から2019年4月30日までである`` (y: int) (m: int) (d: int) (expect: JapaneseEra) =
        DateOnly(y, m, d)
        |> JapaneseEra.fromDateOnly
        |> isSomeOf expect
        |> should be True

    [<Fact>]
    let ``令和は2019年5月1日以降である`` () =
        DateOnly(2019, 5, 1)
        |> JapaneseEra.fromDateOnly
        |> isSomeOf (Reiwa(1))
        |> should be True

    [<Fact>]
    let ``明治より前の元号には対応していない`` () =
        DateOnly(1868, 10, 22)
        |> JapaneseEra.fromDateOnly
        |> Option.isNone
        |> should be True
