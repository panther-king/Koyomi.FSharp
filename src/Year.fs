namespace Koyomi.FSharp

open System

type HeavenlyStem =
    | Kinoe
    | Kinoto
    | Hinoe
    | Hinoto
    | Tsuchinoe
    | Tsuchinoto
    | Kanoe
    | Kanoto
    | Mizunoe
    | Mizunoto

[<RequireQualifiedAccess>]
module HeavenlyStem =
    let private stems = [| "甲"; "乙"; "丙"; "丁"; "戊"; "己"; "庚"; "辛"; "壬"; "癸" |]

    let fromNumber (number: int) =
        match number with
        | 1 -> Some(Kinoe)
        | 2 -> Some(Kinoto)
        | 3 -> Some(Hinoe)
        | 4 -> Some(Hinoto)
        | 5 -> Some(Tsuchinoe)
        | 6 -> Some(Tsuchinoto)
        | 7 -> Some(Kanoe)
        | 8 -> Some(Kanoto)
        | 9 -> Some(Mizunoe)
        | 10 -> Some(Mizunoto)
        | _ -> None

    let fromName (name: string) =
        stems
        |> Array.tryFindIndex (fun n -> n = name)
        |> Option.bind (fun i -> fromNumber (i + 1))

    let number (stem: HeavenlyStem) =
        match stem with
        | Kinoe -> 1
        | Kinoto -> 2
        | Hinoe -> 3
        | Hinoto -> 4
        | Tsuchinoe -> 5
        | Tsuchinoto -> 6
        | Kanoe -> 7
        | Kanoto -> 8
        | Mizunoe -> 9
        | Mizunoto -> 10

    let name (stem: HeavenlyStem) = stems[number stem - 1]

    let fromDateOnly (date: DateOnly) =
        let index =
            match (date.Year + 7) % 10 with
            | 0 -> 10
            | n -> Math.Abs n

        fromNumber index |> Option.get

type JapaneseZodiac =
    | Ne
    | Ushi
    | Tora
    | Wu
    | Tatsu
    | Mi
    | Uma
    | Hitsuji
    | Saru
    | Tori
    | Inu
    | Yi

[<RequireQualifiedAccess>]
module JapaneseZodiac =
    let private japaneseZodiac =
        [| "子"; "丑"; "寅"; "卯"; "辰"; "巳"; "午"; "未"; "申"; "酉"; "戌"; "亥" |]

    let fromNumber (number: int) =
        match number with
        | 1 -> Some(Ne)
        | 2 -> Some(Ushi)
        | 3 -> Some(Tora)
        | 4 -> Some(Wu)
        | 5 -> Some(Tatsu)
        | 6 -> Some(Mi)
        | 7 -> Some(Uma)
        | 8 -> Some(Hitsuji)
        | 9 -> Some(Saru)
        | 10 -> Some(Tori)
        | 11 -> Some(Inu)
        | 12 -> Some(Yi)
        | _ -> None

    let fromName (name: string) =
        japaneseZodiac
        |> Array.tryFindIndex (fun n -> n = name)
        |> Option.bind (fun i -> fromNumber (i + 1))

    let number (zodiac: JapaneseZodiac) =
        match zodiac with
        | Ne -> 1
        | Ushi -> 2
        | Tora -> 3
        | Wu -> 4
        | Tatsu -> 5
        | Mi -> 6
        | Uma -> 7
        | Hitsuji -> 8
        | Saru -> 9
        | Tori -> 10
        | Inu -> 11
        | Yi -> 12

    let name (zodiac: JapaneseZodiac) = japaneseZodiac[number zodiac - 1]

    let fromDateOnly (date: DateOnly) =
        let index =
            match (date.Year + 9) % 12 with
            | 0 -> 12
            | n -> Math.Abs n

        fromNumber index |> Option.get

type SexagenaryCycle =
    | KinoeNe
    | KinotoUshi
    | HinoeTora
    | HinotoWu
    | TsuchinoeTatsu
    | TsuchinotoMi
    | KanoeUma
    | KanotoHitsuji
    | MizunoeSaru
    | MizunotoTori
    | KinoeInu
    | KinotoYi
    | HinoeNe
    | HinotoUshi
    | TsuchinoeTora
    | TsuchinotoWu
    | KanoeTatsu
    | KanotoMi
    | MizunoeUma
    | MizunotoHitsuji
    | KinoeSaru
    | KinotoTori
    | HinoeInu
    | HinotoYi
    | TsuchinoeNe
    | TsuchinotoUshi
    | KanoeTora
    | KanotoWu
    | MizunoeTatsu
    | MizunotoMi
    | KinoeUma
    | KinotoHitsuji
    | HinoeSaru
    | HinotoTori
    | TsuchinoeInu
    | TsuchinotoYi
    | KanoeNe
    | KanotoUshi
    | MizunoeTora
    | MizunotoWu
    | KinoeTatsu
    | KinotoMi
    | HinoeUma
    | HinotoHitsuji
    | TsuchinoeSaru
    | TsuchinotoTori
    | KanoeInu
    | KanotoYi
    | MizunoeNe
    | MizunotoUshi
    | KinoeTora
    | KinotoWu
    | HinoeTatsu
    | HinotoMi
    | TsuchinoeUma
    | TsuchinotoHitsuji
    | KanoeSaru
    | KanotoTori
    | MizunoeInu
    | MizunotoYi

[<RequireQualifiedAccess>]
module SexagenaryCycle =
    let private cycle =
        [| "甲子"
           "乙丑"
           "丙寅"
           "丁卯"
           "戊辰"
           "己巳"
           "庚午"
           "辛未"
           "壬申"
           "癸酉"
           "甲戌"
           "乙亥"
           "丙子"
           "丁丑"
           "戊寅"
           "己卯"
           "庚辰"
           "辛巳"
           "壬午"
           "癸未"
           "甲申"
           "乙酉"
           "丙戌"
           "丁亥"
           "戊子"
           "己丑"
           "庚寅"
           "辛卯"
           "壬辰"
           "癸巳"
           "甲午"
           "乙未"
           "丙申"
           "丁酉"
           "戊戌"
           "己亥"
           "庚子"
           "辛丑"
           "壬寅"
           "癸卯"
           "甲辰"
           "乙巳"
           "丙午"
           "丁未"
           "戊申"
           "己酉"
           "庚戌"
           "辛亥"
           "壬子"
           "癸丑"
           "甲寅"
           "乙卯"
           "丙辰"
           "丁巳"
           "戊午"
           "己未"
           "庚申"
           "辛酉"
           "壬戌"
           "癸亥" |]

    let fromNumber (number: int) =
        match number with
        | 1 -> Some(KinoeNe)
        | 2 -> Some(KinotoUshi)
        | 3 -> Some(HinoeTora)
        | 4 -> Some(HinotoWu)
        | 5 -> Some(TsuchinoeTatsu)
        | 6 -> Some(TsuchinotoMi)
        | 7 -> Some(KanoeUma)
        | 8 -> Some(KanotoHitsuji)
        | 9 -> Some(MizunoeSaru)
        | 10 -> Some(MizunotoTori)
        | 11 -> Some(KinoeInu)
        | 12 -> Some(KinotoYi)
        | 13 -> Some(HinoeNe)
        | 14 -> Some(HinotoUshi)
        | 15 -> Some(TsuchinoeTora)
        | 16 -> Some(TsuchinotoWu)
        | 17 -> Some(KanoeTatsu)
        | 18 -> Some(KanotoMi)
        | 19 -> Some(MizunoeUma)
        | 20 -> Some(MizunotoHitsuji)
        | 21 -> Some(KinoeSaru)
        | 22 -> Some(KinotoTori)
        | 23 -> Some(HinoeInu)
        | 24 -> Some(HinotoYi)
        | 25 -> Some(TsuchinoeNe)
        | 26 -> Some(TsuchinotoUshi)
        | 27 -> Some(KanoeTora)
        | 28 -> Some(KanotoWu)
        | 29 -> Some(MizunoeTatsu)
        | 30 -> Some(MizunotoMi)
        | 31 -> Some(KinoeUma)
        | 32 -> Some(KinotoHitsuji)
        | 33 -> Some(HinoeSaru)
        | 34 -> Some(HinotoTori)
        | 35 -> Some(TsuchinoeInu)
        | 36 -> Some(TsuchinotoYi)
        | 37 -> Some(KanoeNe)
        | 38 -> Some(KanotoUshi)
        | 39 -> Some(MizunoeTora)
        | 40 -> Some(MizunotoWu)
        | 41 -> Some(KinoeTatsu)
        | 42 -> Some(KinotoMi)
        | 43 -> Some(HinoeUma)
        | 44 -> Some(HinotoHitsuji)
        | 45 -> Some(TsuchinoeSaru)
        | 46 -> Some(TsuchinotoTori)
        | 47 -> Some(KanoeInu)
        | 48 -> Some(KanotoYi)
        | 49 -> Some(MizunoeNe)
        | 50 -> Some(MizunotoUshi)
        | 51 -> Some(KinoeTora)
        | 52 -> Some(KinotoWu)
        | 53 -> Some(HinoeTatsu)
        | 54 -> Some(HinotoMi)
        | 55 -> Some(TsuchinoeUma)
        | 56 -> Some(TsuchinotoHitsuji)
        | 57 -> Some(KanoeSaru)
        | 58 -> Some(KanotoTori)
        | 59 -> Some(MizunoeInu)
        | 60 -> Some(MizunotoYi)
        | _ -> None

    let fromName (name: string) =
        cycle
        |> Array.tryFindIndex (fun n -> n = name)
        |> Option.bind (fun i -> fromNumber (i + 1))

    let number (sc: SexagenaryCycle) =
        match sc with
        | KinoeNe -> 1
        | KinotoUshi -> 2
        | HinoeTora -> 3
        | HinotoWu -> 4
        | TsuchinoeTatsu -> 5
        | TsuchinotoMi -> 6
        | KanoeUma -> 7
        | KanotoHitsuji -> 8
        | MizunoeSaru -> 9
        | MizunotoTori -> 10
        | KinoeInu -> 11
        | KinotoYi -> 12
        | HinoeNe -> 13
        | HinotoUshi -> 14
        | TsuchinoeTora -> 15
        | TsuchinotoWu -> 16
        | KanoeTatsu -> 17
        | KanotoMi -> 18
        | MizunoeUma -> 19
        | MizunotoHitsuji -> 20
        | KinoeSaru -> 21
        | KinotoTori -> 22
        | HinoeInu -> 23
        | HinotoYi -> 24
        | TsuchinoeNe -> 25
        | TsuchinotoUshi -> 26
        | KanoeTora -> 27
        | KanotoWu -> 28
        | MizunoeTatsu -> 29
        | MizunotoMi -> 30
        | KinoeUma -> 31
        | KinotoHitsuji -> 32
        | HinoeSaru -> 33
        | HinotoTori -> 34
        | TsuchinoeInu -> 35
        | TsuchinotoYi -> 36
        | KanoeNe -> 37
        | KanotoUshi -> 38
        | MizunoeTora -> 39
        | MizunotoWu -> 40
        | KinoeTatsu -> 41
        | KinotoMi -> 42
        | HinoeUma -> 43
        | HinotoHitsuji -> 44
        | TsuchinoeSaru -> 45
        | TsuchinotoTori -> 46
        | KanoeInu -> 47
        | KanotoYi -> 48
        | MizunoeNe -> 49
        | MizunotoUshi -> 50
        | KinoeTora -> 51
        | KinotoWu -> 52
        | HinoeTatsu -> 53
        | HinotoMi -> 54
        | TsuchinoeUma -> 55
        | TsuchinotoHitsuji -> 56
        | KanoeSaru -> 57
        | KanotoTori -> 58
        | MizunoeInu -> 59
        | MizunotoYi -> 60

    let name (sc: SexagenaryCycle) = cycle[number sc - 1]

    let fromDateOnly (date: DateOnly) =
        let hs = HeavenlyStem.fromDateOnly date
        let jz = JapaneseZodiac.fromDateOnly date
        let sc = sprintf "%s%s" (HeavenlyStem.name hs) (JapaneseZodiac.name jz)
        fromName sc |> Option.get
