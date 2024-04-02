namespace Koyomi.FSharp.Tests

open System

open Xunit
open FsUnit.Xunit

open Koyomi.FSharp

module HeavenlyStemTests =
    [<Fact>]
    let ``年月日から変換できる`` () =
        DateOnly(2024, 1, 1) |> HeavenlyStem.fromDateOnly |> should equal Kinoe

    let namesOfStem: obj[] list =
        [ [| "甲"; Kinoe |]
          [| "乙"; Kinoto |]
          [| "丙"; Hinoe |]
          [| "丁"; Hinoto |]
          [| "戊"; Tsuchinoe |]
          [| "己"; Tsuchinoto |]
          [| "庚"; Kanoe |]
          [| "辛"; Kanoto |]
          [| "壬"; Mizunoe |]
          [| "癸"; Mizunoto |] ]

    [<Theory>]
    [<MemberData("namesOfStem")>]
    let ``十干の名前から変換できる`` (name: string) (expect: HeavenlyStem) =
        HeavenlyStem.fromName name |> isSomeOf expect |> should be True

    [<Theory>]
    [<MemberData("namesOfStem")>]
    let ``十干の名前に変換できる`` (expect: string) (stem: HeavenlyStem) =
        HeavenlyStem.name stem |> should equal expect

    [<Fact>]
    let ``十干の名前でなければ変換できない`` () =
        HeavenlyStem.fromName String.Empty |> should equal None

    let numbersOfStem: obj[] list =
        [ [| 1; Kinoe |]
          [| 2; Kinoto |]
          [| 3; Hinoe |]
          [| 4; Hinoto |]
          [| 5; Tsuchinoe |]
          [| 6; Tsuchinoto |]
          [| 7; Kanoe |]
          [| 8; Kanoto |]
          [| 9; Mizunoe |]
          [| 10; Mizunoto |] ]

    [<Theory>]
    [<MemberData("numbersOfStem")>]
    let ``十干の番号から変換できる`` (number: int) (expect: HeavenlyStem) =
        HeavenlyStem.fromNumber number |> isSomeOf expect |> should be True

    [<Theory>]
    [<MemberData("numbersOfStem")>]
    let ``十干の番号に変換できる`` (expect: int) (stem: HeavenlyStem) =
        HeavenlyStem.number stem |> should equal expect

    [<Fact>]
    let ``十干の番号でなければ変換できない`` () =
        HeavenlyStem.fromNumber 0 |> should equal None

module JapaneseZodiacTests =
    let yearsOfZodiac: obj[] list =
        [ [| 2020; Ne |]
          [| 2021; Ushi |]
          [| 2022; Tora |]
          [| 2023; Wu |]
          [| 2024; Tatsu |]
          [| 2025; Mi |]
          [| 2026; Uma |]
          [| 2027; Hitsuji |]
          [| 2028; Saru |]
          [| 2029; Tori |]
          [| 2030; Inu |]
          [| 2031; Yi |] ]

    [<Theory>]
    [<MemberData("yearsOfZodiac")>]
    let ``西暦から十二支を導出できる`` (y: int) (expect: JapaneseZodiac) =
        DateOnly(y, 1, 1) |> JapaneseZodiac.fromDateOnly |> should equal expect

    let namesOfZodiac: obj[] list =
        [ [| "子"; Ne |]
          [| "丑"; Ushi |]
          [| "寅"; Tora |]
          [| "卯"; Wu |]
          [| "辰"; Tatsu |]
          [| "巳"; Mi |]
          [| "午"; Uma |]
          [| "未"; Hitsuji |]
          [| "申"; Saru |]
          [| "酉"; Tori |]
          [| "戌"; Inu |]
          [| "亥"; Yi |] ]

    [<Theory>]
    [<MemberData("namesOfZodiac")>]
    let ``十二支の名前から変換できる`` (name: string) (expect: JapaneseZodiac) =
        JapaneseZodiac.fromName name |> isSomeOf expect |> should be True

    [<Theory>]
    [<MemberData("namesOfZodiac")>]
    let ``十二支の名前に変換できる`` (expect: string) (zodiac: JapaneseZodiac) =
        JapaneseZodiac.name zodiac |> should equal expect

    [<Fact>]
    let ``十二支の名前でなければ変換できない`` () =
        JapaneseZodiac.fromName String.Empty |> should equal None

    let numbersOfZodiac: obj[] list =
        [ [| 1; Ne |]
          [| 2; Ushi |]
          [| 3; Tora |]
          [| 4; Wu |]
          [| 5; Tatsu |]
          [| 6; Mi |]
          [| 7; Uma |]
          [| 8; Hitsuji |]
          [| 9; Saru |]
          [| 10; Tori |]
          [| 11; Inu |]
          [| 12; Yi |] ]

    [<Theory>]
    [<MemberData("numbersOfZodiac")>]
    let ``十二支の番号から変換できる`` (number: int) (expect: JapaneseZodiac) =
        JapaneseZodiac.fromNumber number |> isSomeOf expect |> should be True

    [<Theory>]
    [<MemberData("numbersOfZodiac")>]
    let ``十二支の番号に変換できる`` (expect: int) (zodiac: JapaneseZodiac) =
        JapaneseZodiac.number zodiac |> should equal expect

    [<Fact>]
    let ``十二支の番号でなければ変換できない`` () =
        JapaneseZodiac.fromNumber 0 |> should equal None

module SexagenaryCycleTests =
    [<Fact>]
    let ``年月日から変換できる`` () =
        DateOnly(2024, 1, 1) |> SexagenaryCycle.fromDateOnly |> should equal KinoeTatsu

    let namesOfCycle: obj[] list =
        [ [| "甲子"; KinoeNe |]
          [| "乙丑"; KinotoUshi |]
          [| "丙寅"; HinoeTora |]
          [| "丁卯"; HinotoWu |]
          [| "戊辰"; TsuchinoeTatsu |]
          [| "己巳"; TsuchinotoMi |]
          [| "庚午"; KanoeUma |]
          [| "辛未"; KanotoHitsuji |]
          [| "壬申"; MizunoeSaru |]
          [| "癸酉"; MizunotoTori |]
          [| "甲戌"; KinoeInu |]
          [| "乙亥"; KinotoYi |]
          [| "丙子"; HinoeNe |]
          [| "丁丑"; HinotoUshi |]
          [| "戊寅"; TsuchinoeTora |]
          [| "己卯"; TsuchinotoWu |]
          [| "庚辰"; KanoeTatsu |]
          [| "辛巳"; KanotoMi |]
          [| "壬午"; MizunoeUma |]
          [| "癸未"; MizunotoHitsuji |]
          [| "甲申"; KinoeSaru |]
          [| "乙酉"; KinotoTori |]
          [| "丙戌"; HinoeInu |]
          [| "丁亥"; HinotoYi |]
          [| "戊子"; TsuchinoeNe |]
          [| "己丑"; TsuchinotoUshi |]
          [| "庚寅"; KanoeTora |]
          [| "辛卯"; KanotoWu |]
          [| "壬辰"; MizunoeTatsu |]
          [| "癸巳"; MizunotoMi |]
          [| "甲午"; KinoeUma |]
          [| "乙未"; KinotoHitsuji |]
          [| "丙申"; HinoeSaru |]
          [| "丁酉"; HinotoTori |]
          [| "戊戌"; TsuchinoeInu |]
          [| "己亥"; TsuchinotoYi |]
          [| "庚子"; KanoeNe |]
          [| "辛丑"; KanotoUshi |]
          [| "壬寅"; MizunoeTora |]
          [| "癸卯"; MizunotoWu |]
          [| "甲辰"; KinoeTatsu |]
          [| "乙巳"; KinotoMi |]
          [| "丙午"; HinoeUma |]
          [| "丁未"; HinotoHitsuji |]
          [| "戊申"; TsuchinoeSaru |]
          [| "己酉"; TsuchinotoTori |]
          [| "庚戌"; KanoeInu |]
          [| "辛亥"; KanotoYi |]
          [| "壬子"; MizunoeNe |]
          [| "癸丑"; MizunotoUshi |]
          [| "甲寅"; KinoeTora |]
          [| "乙卯"; KinotoWu |]
          [| "丙辰"; HinoeTatsu |]
          [| "丁巳"; HinotoMi |]
          [| "戊午"; TsuchinoeUma |]
          [| "己未"; TsuchinotoHitsuji |]
          [| "庚申"; KanoeSaru |]
          [| "辛酉"; KanotoTori |]
          [| "壬戌"; MizunoeInu |]
          [| "癸亥"; MizunotoYi |] ]

    [<Theory>]
    [<MemberData("namesOfCycle")>]
    let ``六十干支の名前から変換できる`` (name: string) (expect: SexagenaryCycle) =
        SexagenaryCycle.fromName name |> isSomeOf expect |> should be True

    [<Theory>]
    [<MemberData("namesOfCycle")>]
    let ``六十干支の名前に変換できる`` (expect: string) (cycle: SexagenaryCycle) =
        SexagenaryCycle.name cycle |> should equal expect

    [<Fact>]
    let ``六十干支の名前でなければ変換できない`` () =
        SexagenaryCycle.fromName String.Empty |> should equal None
