namespace Koyomi.FSharp.Tests

open System

open Xunit
open FsUnit.Xunit

open Koyomi.FSharp

module HolidayNameTests =
    let pair: obj[] list =
        [ [| AutumnalEquinoxDay; "秋分の日" |]
          [| ChildrensDay; "こどもの日" |]
          [| ComingOfAgeDay; "成人の日" |]
          [| ConstitutionDay; "憲法記念日" |]
          [| CultureDay; "文化の日" |]
          [| EmperorsBirthday; "天皇誕生日" |]
          [| EnthronmentCeremonyOfEmperorHeisei; "即位礼正殿の儀" |]
          [| EnthronmentCeremonyOfEmperorReiwa; "即位礼正殿の儀" |]
          [| EnthronmentOfEmperorReiwa; "天皇即位" |]
          [| GreenDay; "みどりの日" |]
          [| LaborThanksgivingDay; "勤労感謝の日" |]
          [| MarineDay; "海の日" |]
          [| MountainDay; "山の日" |]
          [| MouringCeremonyOfEmperorShowa; "昭和天皇大喪の礼" |]
          [| NationalFoundationDay; "建国記念の日" |]
          [| NewYearsDay; "元日" |]
          [| PhysicalEducationDay; "体育の日" |]
          [| RespectForTheAgeDay; "敬老の日" |]
          [| ShowaDay; "昭和の日" |]
          [| SportsDay; "スポーツの日" |]
          [| SubstituteDay; "振替休日" |]
          [| VernalEquinoxDay; "春分の日" |]
          [| WeddingCeremonyOfPrinceAkihito; "明仁親王の結婚の儀" |]
          [| WeddingCeremonyOfPrinceNaruhito; "徳仁親王の結婚の儀" |] ]

    [<Theory>]
    [<MemberData("pair")>]
    let ``祝日名に変換できる`` (seed: JapaneseHoliday) (expect: string) =
        JapaneseHoliday.holidayName seed |> should equal expect

module NewYearsDayTests =
    let afterEstablished: obj[] list =
        [ 1949 .. DateTime.Now.Year ] |> List.map (fun y -> [| y |])

    [<Theory>]
    [<MemberData("afterEstablished")>]
    let ``祝日法の施行後、1月1日は元日である`` (y: int) =
        DateOnly(y, 1, 1)
        |> JapaneseHoliday.newYearsDay
        |> isSomeOf NewYearsDay
        |> should be True

    [<Fact>]
    let ``祝日法の施行以前は、1月1日であっても元日ではない`` () =
        DateOnly(1948, 1, 1)
        |> JapaneseHoliday.newYearsDay
        |> isSomeOf NewYearsDay
        |> should be False

module ComingOfAgeDayTests =
    let afterEstablished: obj[] list = [ 1949..1999 ] |> List.map (fun y -> [| y |])

    [<Theory>]
    [<MemberData("afterEstablished")>]
    let ``祝日法の施行後、1月15日は成人の日である`` (y: int) =
        DateOnly(y, 1, 15)
        |> JapaneseHoliday.comingOfAgeDay
        |> isSomeOf ComingOfAgeDay
        |> should be True

    [<Fact>]
    let ``祝日法の施行以前は、1月15日であっても成人の日ではない`` () =
        DateOnly(1948, 1, 15)
        |> JapaneseHoliday.comingOfAgeDay
        |> isSomeOf ComingOfAgeDay
        |> should be False

    let afterHappyMonday: obj[] list =
        [ [| 2000; 10 |]
          [| 2001; 8 |]
          [| 2002; 14 |]
          [| 2003; 13 |]
          [| 2004; 12 |]
          [| 2005; 10 |]
          [| 2006; 9 |]
          [| 2007; 8 |]
          [| 2008; 14 |]
          [| 2009; 12 |]
          [| 2010; 11 |]
          [| 2011; 10 |]
          [| 2012; 9 |]
          [| 2013; 14 |]
          [| 2014; 13 |]
          [| 2015; 12 |]
          [| 2016; 11 |]
          [| 2017; 9 |]
          [| 2018; 8 |]
          [| 2019; 14 |]
          [| 2020; 13 |]
          [| 2021; 11 |]
          [| 2022; 10 |]
          [| 2023; 9 |]
          [| 2024; 8 |] ]

    [<Theory>]
    [<MemberData("afterHappyMonday")>]
    let ``祝日法の改正後は、1月の第2月曜が成人の日である`` (y: int, d: int) =
        DateOnly(y, 1, d)
        |> JapaneseHoliday.comingOfAgeDay
        |> isSomeOf ComingOfAgeDay
        |> should be True

    [<Fact>]
    let ``祝日法の改正後は、1月15日であっても第2月曜でなければ成人の日ではない`` () =
        DateOnly(2000, 1, 15)
        |> JapaneseHoliday.comingOfAgeDay
        |> isSomeOf ComingOfAgeDay
        |> should be False

module NationalFoundationDayTests =
    let afterRevision: obj[] list =
        [ 1967 .. DateTime.Now.Year ] |> List.map (fun y -> [| y |])

    [<Theory>]
    [<MemberData("afterRevision")>]
    let ``祝日法の改正後は、2月11日が建国記念の日である`` (y: int) =
        DateOnly(y, 2, 11)
        |> JapaneseHoliday.nationalFoundationDay
        |> isSomeOf NationalFoundationDay
        |> should be True

    [<Fact>]
    let ``祝日法の改正以前は、2月11日であっても建国記念の日ではない`` () =
        DateOnly(1966, 2, 11)
        |> JapaneseHoliday.nationalFoundationDay
        |> isSomeOf NationalFoundationDay
        |> should be False

module EmperorsBirthdayTests =
    let emperorShowa: obj[] list = [ 1949..1988 ] |> List.map (fun y -> [| y |])

    [<Theory>]
    [<MemberData("emperorShowa")>]
    let ``祝日法の施行後で昭和天皇在位時は、4月29日が天皇誕生日である`` (y: int) =
        DateOnly(y, 4, 29)
        |> JapaneseHoliday.emperorsBirthday
        |> isSomeOf EmperorsBirthday
        |> should be True

    let emperorHeisei: obj[] list = [ 1989..2018 ] |> List.map (fun y -> [| y |])

    [<Theory>]
    [<MemberData("emperorHeisei")>]
    let ``平成天皇在位時は、12月23日が天皇誕生日である`` (y: int) =
        DateOnly(y, 12, 23)
        |> JapaneseHoliday.emperorsBirthday
        |> isSomeOf EmperorsBirthday
        |> should be True

    let emperorReiwa: obj[] list =
        [ 2020 .. DateTime.Now.Year ] |> List.map (fun y -> [| y |])

    [<Theory>]
    [<MemberData("emperorReiwa")>]
    let ``令和天皇在位時は、2月23日が天皇誕生日である`` (y: int) =
        DateOnly(y, 2, 23)
        |> JapaneseHoliday.emperorsBirthday
        |> isSomeOf EmperorsBirthday
        |> should be True

    let emperorAbdication: obj[] list = [ [| 2; 23 |]; [| 12; 23 |] ]

    [<Theory>]
    [<MemberData("emperorAbdication")>]
    let ``平成天皇退位の2019年は、天皇誕生日が存在しない`` (m: int, d: int) =
        DateOnly(2019, m, d)
        |> JapaneseHoliday.emperorsBirthday
        |> isSomeOf EmperorsBirthday
        |> should be False

    [<Fact>]
    let ``祝日法の施行前は、昭和天皇在位時の4月29日でも天皇誕生日ではない`` () =
        DateOnly(1948, 4, 29)
        |> JapaneseHoliday.emperorsBirthday
        |> isSomeOf EmperorsBirthday
        |> should be False

module VernalEquinoxDayTests =
    let afterEstablished: obj[] list =
        [ [| 1949; 21 |]
          [| 1950; 21 |]
          [| 1951; 21 |]
          [| 1952; 21 |]
          [| 1953; 21 |]
          [| 1954; 21 |]
          [| 1955; 21 |]
          [| 1956; 21 |]
          [| 1957; 21 |]
          [| 1958; 21 |]
          [| 1959; 21 |]
          [| 1960; 20 |]
          [| 1961; 21 |]
          [| 1962; 21 |]
          [| 1963; 21 |]
          [| 1964; 20 |]
          [| 1965; 21 |]
          [| 1966; 21 |]
          [| 1967; 21 |]
          [| 1968; 20 |]
          [| 1969; 21 |]
          [| 1970; 21 |]
          [| 1971; 21 |]
          [| 1972; 20 |]
          [| 1973; 21 |]
          [| 1974; 21 |]
          [| 1975; 21 |]
          [| 1976; 20 |]
          [| 1977; 21 |]
          [| 1978; 21 |]
          [| 1979; 21 |]
          [| 1980; 20 |]
          [| 1981; 21 |]
          [| 1982; 21 |]
          [| 1983; 21 |]
          [| 1984; 20 |]
          [| 1985; 21 |]
          [| 1986; 21 |]
          [| 1987; 21 |]
          [| 1988; 20 |]
          [| 1989; 21 |]
          [| 1990; 21 |]
          [| 1991; 21 |]
          [| 1992; 20 |]
          [| 1993; 20 |]
          [| 1994; 21 |]
          [| 1995; 21 |]
          [| 1996; 20 |]
          [| 1997; 20 |]
          [| 1998; 21 |]
          [| 1999; 21 |]
          [| 2000; 20 |]
          [| 2001; 20 |]
          [| 2002; 21 |]
          [| 2003; 21 |]
          [| 2004; 20 |]
          [| 2005; 20 |]
          [| 2006; 21 |]
          [| 2007; 21 |]
          [| 2008; 20 |]
          [| 2009; 20 |]
          [| 2010; 21 |]
          [| 2011; 21 |]
          [| 2012; 20 |]
          [| 2013; 20 |]
          [| 2014; 21 |]
          [| 2015; 21 |]
          [| 2016; 20 |]
          [| 2017; 20 |]
          [| 2018; 21 |]
          [| 2019; 21 |]
          [| 2020; 20 |]
          [| 2021; 20 |]
          [| 2022; 21 |]
          [| 2023; 21 |]
          [| 2024; 20 |] ]

    [<Theory>]
    [<MemberData("afterEstablished")>]
    let ``祝日法の施行後、春分日は春分の日である`` (y: int, d: int) =
        DateOnly(y, 3, d)
        |> JapaneseHoliday.vernalEquinoxDay
        |> isSomeOf VernalEquinoxDay
        |> should be True

    [<Fact>]
    let ``祝日法の施行以前は、春分日であっても春分の日ではない`` () =
        DateOnly(1948, 3, 21)
        |> JapaneseHoliday.vernalEquinoxDay
        |> isSomeOf VernalEquinoxDay
        |> should be False

module GreenDayTests =
    let beforeShowaDay: obj[] list = [ 1989..2006 ] |> List.map (fun y -> [| y |])

    [<Theory>]
    [<MemberData("beforeShowaDay")>]
    let ``昭和天皇崩御から昭和の日制定まで、4月29日はみどりの日である`` (y: int) =
        DateOnly(y, 4, 29)
        |> JapaneseHoliday.greenDay
        |> isSomeOf GreenDay
        |> should be True

    let afterShowaDay: obj[] list =
        [ 2007 .. DateTime.Now.Year ] |> List.map (fun y -> [| y |])

    [<Theory>]
    [<MemberData("afterShowaDay")>]
    let ``昭和の日制定後は、5月4日がみどりの日である`` (y: int) =
        DateOnly(y, 5, 4)
        |> JapaneseHoliday.greenDay
        |> isSomeOf GreenDay
        |> should be True

    [<Fact>]
    let ``昭和天皇在位時の4月29日は、みどりの日ではない`` () =
        DateOnly(1988, 4, 29)
        |> JapaneseHoliday.greenDay
        |> isSomeOf GreenDay
        |> should be False

module ShowaDayTests =
    let afterEstablished: obj[] list =
        [ 2007 .. DateTime.Now.Year ] |> List.map (fun y -> [| y |])

    [<Theory>]
    [<MemberData("afterEstablished")>]
    let ``昭和の日制定された2007年以降は、4月29日が昭和の日である`` (y: int) =
        DateOnly(y, 4, 29)
        |> JapaneseHoliday.showaDay
        |> isSomeOf ShowaDay
        |> should be True

    [<Fact>]
    let ``昭和の日制定以前の4月29日は、昭和の日ではない`` () =
        DateOnly(2006, 4, 29)
        |> JapaneseHoliday.showaDay
        |> isSomeOf ShowaDay
        |> should be False

module ConstitutionDayTests =
    let afterEstablished: obj[] list =
        [ 1948 .. DateTime.Now.Year ] |> List.map (fun y -> [| y |])

    [<Theory>]
    [<MemberData("afterEstablished")>]
    let ``祝日法の施行後、5月3日は憲法記念日である`` (y: int) =
        DateOnly(y, 5, 3)
        |> JapaneseHoliday.constitutionDay
        |> isSomeOf ConstitutionDay
        |> should be True

    [<Fact>]
    let ``祝日法施行以前の5月3日は、憲法記念日ではない`` () =
        DateOnly(1947, 5, 3)
        |> JapaneseHoliday.constitutionDay
        |> isSomeOf ConstitutionDay
        |> should be False

module ChildrensDayTests =
    let afterEstablished: obj[] list =
        [ 1948 .. DateTime.Now.Year ] |> List.map (fun y -> [| y |])

    [<Theory>]
    [<MemberData("afterEstablished")>]
    let ``祝日法の施行後、5月5日はこどもの日である`` (y: int) =
        DateOnly(y, 5, 5)
        |> JapaneseHoliday.childrensDay
        |> isSomeOf ChildrensDay
        |> should be True

    [<Fact>]
    let ``祝日法施行以前の5月5日は、こどもの日ではない`` () =
        DateOnly(1947, 5, 5)
        |> JapaneseHoliday.childrensDay
        |> isSomeOf ChildrensDay
        |> should be False

module MarineDayTests =
    let afterEstablished: obj[] list = [ 1996..2002 ] |> List.map (fun y -> [| y |])

    [<Theory>]
    [<MemberData("afterEstablished")>]
    let ``制定年の1996年からハッピーマンデー導入の2002年まで、7月20日は海の日である`` (y: int) =
        DateOnly(y, 7, 20)
        |> JapaneseHoliday.marineDay
        |> isSomeOf MarineDay
        |> should be True

    let afterHappyMonday: obj[] list =
        [ [| 2003; 21 |]
          [| 2004; 19 |]
          [| 2005; 18 |]
          [| 2006; 17 |]
          [| 2007; 16 |]
          [| 2008; 21 |]
          [| 2009; 20 |]
          [| 2010; 19 |]
          [| 2011; 18 |]
          [| 2012; 16 |]
          [| 2013; 15 |]
          [| 2014; 21 |]
          [| 2015; 20 |]
          [| 2016; 18 |]
          [| 2017; 17 |]
          [| 2018; 16 |]
          [| 2019; 15 |] ]

    [<Theory>]
    [<MemberData("afterHappyMonday")>]
    let ``ハッピーマンデー導入から2019年まで、7月の第3月曜が海の日である`` (y: int, d: int) =
        DateOnly(y, 7, d)
        |> JapaneseHoliday.marineDay
        |> isSomeOf MarineDay
        |> should be True

    [<Fact>]
    let ``東京五輪の特措法に基づき、2020年は7月23日が海の日である`` () =
        DateOnly(2020, 7, 23)
        |> JapaneseHoliday.marineDay
        |> isSomeOf MarineDay
        |> should be True

    [<Fact>]
    let ``東京五輪開催年の2021年は、開会式前日の7月22日が海の日である`` () =
        DateOnly(2021, 7, 22)
        |> JapaneseHoliday.marineDay
        |> isSomeOf MarineDay
        |> should be True

    let afterTokyoOlympic: obj[] list =
        [ [| 2022; 18 |]; [| 2023; 17 |]; [| 2024; 15 |] ]

    [<Theory>]
    [<MemberData("afterTokyoOlympic")>]
    let ``東京五輪以降は、7月の第3月曜が海の日である`` (y: int, d: int) =
        DateOnly(y, 7, d)
        |> JapaneseHoliday.marineDay
        |> isSomeOf MarineDay
        |> should be True

    [<Fact>]
    let ``制定以前の7月20日は、海の日ではない`` () =
        DateOnly(1995, 7, 20)
        |> JapaneseHoliday.marineDay
        |> isSomeOf MarineDay
        |> should be False

module MountainDayTests =
    let afterEstablished: obj[] list = [ 2016..2019 ] |> List.map (fun y -> [| y |])

    [<Theory>]
    [<MemberData("afterEstablished")>]
    let ``制定年の2016年から2019年まで、8月11日は山の日である`` (y: int) =
        DateOnly(y, 8, 11)
        |> JapaneseHoliday.mountainDay
        |> isSomeOf MountainDay
        |> should be True

    [<Fact>]
    let ``東京五輪の特措法に基づき、2020年は8月10日が山の日である`` () =
        DateOnly(2020, 8, 10)
        |> JapaneseHoliday.mountainDay
        |> isSomeOf MountainDay
        |> should be True

    [<Fact>]
    let ``東京五輪開催の2021年は、閉会式翌日の8月8日が山の日である`` () =
        DateOnly(2021, 8, 8)
        |> JapaneseHoliday.mountainDay
        |> isSomeOf MountainDay
        |> should be True

    let afterTokyoOlympic: obj[] list =
        [ 2022 .. DateTime.Now.Year ] |> List.map (fun y -> [| y |])

    [<Theory>]
    [<MemberData("afterTokyoOlympic")>]
    let ``東京五輪以降は、8月11日が山の日である`` (y: int) =
        DateOnly(y, 8, 11)
        |> JapaneseHoliday.mountainDay
        |> isSomeOf MountainDay
        |> should be True

    [<Fact>]
    let ``制定以前の8月11日は、山の日ではない`` () =
        DateOnly(2015, 8, 11)
        |> JapaneseHoliday.mountainDay
        |> isSomeOf MountainDay
        |> should be False

module RespectForTheAgeDayTests =
    let afterEstablished: obj[] list = [ 1966..2002 ] |> List.map (fun y -> [| y |])

    [<Theory>]
    [<MemberData("afterEstablished")>]
    let ``制定年の1966年からハッピーマンデー導入の2002年まで、9月15日は敬老の日である`` (y: int) =
        DateOnly(y, 9, 15)
        |> JapaneseHoliday.respectForTheAgeDay
        |> isSomeOf RespectForTheAgeDay
        |> should be True

    let afterHappyMonday: obj[] list =
        [ [| 2003; 15 |]
          [| 2004; 20 |]
          [| 2005; 19 |]
          [| 2006; 18 |]
          [| 2007; 17 |]
          [| 2008; 15 |]
          [| 2009; 21 |]
          [| 2010; 20 |]
          [| 2011; 19 |]
          [| 2012; 17 |]
          [| 2013; 16 |]
          [| 2014; 15 |]
          [| 2015; 21 |]
          [| 2016; 19 |]
          [| 2017; 18 |]
          [| 2018; 17 |]
          [| 2019; 16 |]
          [| 2020; 21 |]
          [| 2021; 20 |]
          [| 2022; 19 |]
          [| 2023; 18 |]
          [| 2024; 16 |] ]

    [<Theory>]
    [<MemberData("afterHappyMonday")>]
    let ``ハッピーマンデー導入後は、9月の第3月曜が敬老の日である`` (y: int) (d: int) =
        DateOnly(y, 9, d)
        |> JapaneseHoliday.respectForTheAgeDay
        |> isSomeOf RespectForTheAgeDay
        |> should be True

    [<Fact>]
    let ``制定以前の9月15日は、敬老の日ではない`` () =
        DateOnly(1965, 9, 15)
        |> JapaneseHoliday.respectForTheAgeDay
        |> isSomeOf RespectForTheAgeDay
        |> should be False

module AutumnalEquinoxDayTests =
    let afterEstablished: obj[] list =
        [ [| 1949; 23 |]
          [| 1950; 23 |]
          [| 1951; 24 |]
          [| 1952; 23 |]
          [| 1953; 23 |]
          [| 1954; 23 |]
          [| 1955; 24 |]
          [| 1956; 23 |]
          [| 1957; 23 |]
          [| 1958; 23 |]
          [| 1959; 24 |]
          [| 1960; 23 |]
          [| 1961; 23 |]
          [| 1962; 23 |]
          [| 1963; 24 |]
          [| 1964; 23 |]
          [| 1965; 23 |]
          [| 1966; 23 |]
          [| 1967; 24 |]
          [| 1968; 23 |]
          [| 1969; 23 |]
          [| 1970; 23 |]
          [| 1979; 24 |]
          [| 1980; 23 |]
          [| 1981; 23 |]
          [| 1982; 23 |]
          [| 1983; 23 |]
          [| 1984; 23 |]
          [| 1985; 23 |]
          [| 1986; 23 |]
          [| 1987; 23 |]
          [| 1988; 23 |]
          [| 1989; 23 |]
          [| 1990; 23 |]
          [| 1991; 23 |]
          [| 1992; 23 |]
          [| 1993; 23 |]
          [| 1994; 23 |]
          [| 1995; 23 |]
          [| 1996; 23 |]
          [| 1997; 23 |]
          [| 1998; 23 |]
          [| 1999; 23 |]
          [| 2000; 23 |]
          [| 2001; 23 |]
          [| 2002; 23 |]
          [| 2003; 23 |]
          [| 2004; 23 |]
          [| 2005; 23 |]
          [| 2006; 23 |]
          [| 2007; 23 |]
          [| 2008; 23 |]
          [| 2009; 23 |]
          [| 2010; 23 |]
          [| 2011; 23 |]
          [| 2012; 22 |]
          [| 2013; 23 |]
          [| 2014; 23 |]
          [| 2015; 23 |]
          [| 2016; 22 |]
          [| 2017; 23 |]
          [| 2018; 23 |]
          [| 2019; 23 |]
          [| 2020; 22 |]
          [| 2021; 23 |]
          [| 2022; 23 |]
          [| 2023; 23 |]
          [| 2024; 22 |] ]

    [<Theory>]
    [<MemberData("afterEstablished")>]
    let ``祝日法施行後の秋分日は秋分の日である`` (y: int) (d: int) =
        DateOnly(y, 9, d)
        |> JapaneseHoliday.autumnalEquinoxDay
        |> isSomeOf AutumnalEquinoxDay
        |> should be True

    [<Fact>]
    let ``祝日法施行以前は、秋分日であっても秋分の日ではない`` () =
        DateOnly(1948, 9, 23)
        |> JapaneseHoliday.autumnalEquinoxDay
        |> isSomeOf AutumnalEquinoxDay
        |> should be False

module PhysicalEducationDayTests =
    let afterEstablished: obj[] list = [ 1966..1999 ] |> List.map (fun y -> [| y |])

    [<Theory>]
    [<MemberData("afterEstablished")>]
    let ``制定年の1966年からハッピーマンデー導入の1999年まで、10月10日は体育の日である`` (y: int) =
        DateOnly(y, 10, 10)
        |> JapaneseHoliday.physicalEducationDay
        |> isSomeOf PhysicalEducationDay
        |> should be True

    let afterHappyMonday: obj[] list =
        [ [| 2000; 9 |]
          [| 2001; 8 |]
          [| 2002; 14 |]
          [| 2003; 13 |]
          [| 2004; 11 |]
          [| 2005; 10 |]
          [| 2006; 9 |]
          [| 2007; 8 |]
          [| 2008; 13 |]
          [| 2009; 12 |]
          [| 2010; 11 |]
          [| 2011; 10 |]
          [| 2012; 8 |]
          [| 2013; 14 |]
          [| 2014; 13 |]
          [| 2015; 12 |]
          [| 2016; 10 |]
          [| 2017; 9 |]
          [| 2018; 8 |]
          [| 2019; 14 |] ]

    [<Theory>]
    [<MemberData("afterHappyMonday")>]
    let ``ハッピーマンデー導入から祝日名変更の2019年まで、10月の第2月曜が体育の日である`` (y: int) (d: int) =
        DateOnly(y, 10, d)
        |> JapaneseHoliday.physicalEducationDay
        |> isSomeOf PhysicalEducationDay
        |> should be True

    [<Fact>]
    let ``制定以前の10月10日は体育の日ではない`` () =
        DateOnly(1965, 10, 10)
        |> JapaneseHoliday.physicalEducationDay
        |> isSomeOf PhysicalEducationDay
        |> should be False

module SportsDayTests =
    let afterTokyoOlympic: obj[] list =
        [ [| 2022; 10 |]; [| 2023; 9 |]; [| 2024; 14 |] ]

    [<Fact>]
    let ``東京五輪の特措法に基づき、2020年は7月24日がスポーツの日である`` () =
        DateOnly(2020, 7, 24)
        |> JapaneseHoliday.sportsDay
        |> isSomeOf SportsDay
        |> should be True

    [<Fact>]
    let ``東京オリンピック開催年の2021年は、7月23日がスポーツの日である`` () =
        DateOnly(2021, 7, 23)
        |> JapaneseHoliday.sportsDay
        |> isSomeOf SportsDay
        |> should be True

    [<Theory>]
    [<MemberData("afterTokyoOlympic")>]
    let ``東京オリンピック以降は、10月の第2月曜がスポーツの日である`` (y: int) (d: int) =
        DateOnly(y, 10, d)
        |> JapaneseHoliday.sportsDay
        |> isSomeOf SportsDay
        |> should be True

    [<Fact>]
    let ``制定以前の10月第2月曜は、スポーツの日ではない`` () =
        DateOnly(2019, 10, 14)
        |> JapaneseHoliday.sportsDay
        |> isSomeOf SportsDay
        |> should be False

module CultureDayTests =
    let afterEstablished: obj[] list =
        [ 1948 .. DateTime.Now.Year ] |> List.map (fun y -> [| y |])

    [<Theory>]
    [<MemberData("afterEstablished")>]
    let ``祝日法施行後の11月3日は、文化の日である`` (y: int) =
        DateOnly(y, 11, 3)
        |> JapaneseHoliday.cultureDay
        |> isSomeOf CultureDay
        |> should be True

    [<Fact>]
    let ``祝日法施行以前は、11月3日であっても文化の日ではない`` () =
        DateOnly(1947, 11, 3)
        |> JapaneseHoliday.cultureDay
        |> isSomeOf CultureDay
        |> should be False

module LaborThanksgivingDayTests =
    let afterEstablished: obj[] list =
        [ 1948 .. DateTime.Now.Year ] |> List.map (fun y -> [| y |])

    [<Theory>]
    [<MemberData("afterEstablished")>]
    let ``祝日法施行後の11月23日は、勤労感謝の日である`` (y: int) =
        DateOnly(y, 11, 23)
        |> JapaneseHoliday.laborThanksgivingDay
        |> isSomeOf LaborThanksgivingDay
        |> should be True

    [<Fact>]
    let ``祝日法の施行以前は、11月23日であっても勤労感謝の日ではない`` () =
        DateOnly(1947, 11, 23)
        |> JapaneseHoliday.laborThanksgivingDay
        |> isSomeOf LaborThanksgivingDay
        |> should be False

module ImperialCeremonyTests =
    [<Fact>]
    let ``明仁親王の結婚の儀が行われた1959年4月10日は祝日である`` () =
        DateOnly(1959, 4, 10)
        |> JapaneseHoliday.imperialCeremonyDay
        |> isSomeOf WeddingCeremonyOfPrinceAkihito
        |> should be True

    [<Fact>]
    let ``昭和天皇大喪の礼が行われた1989年2月24日は祝日である`` () =
        DateOnly(1989, 2, 24)
        |> JapaneseHoliday.imperialCeremonyDay
        |> isSomeOf MouringCeremonyOfEmperorShowa
        |> should be True

    [<Fact>]
    let ``平成天皇の即位礼正殿の儀が行われた1990年11月12日は祝日である`` () =
        DateOnly(1990, 11, 12)
        |> JapaneseHoliday.imperialCeremonyDay
        |> isSomeOf EnthronmentCeremonyOfEmperorHeisei
        |> should be True

    [<Fact>]
    let ``徳仁親王の結婚の儀が行われた1993年6月9日は祝日である`` () =
        DateOnly(1993, 6, 9)
        |> JapaneseHoliday.imperialCeremonyDay
        |> isSomeOf WeddingCeremonyOfPrinceNaruhito
        |> should be True

    [<Fact>]
    let ``令和天皇が即位した2019年5月1日は祝日である`` () =
        DateOnly(2019, 5, 1)
        |> JapaneseHoliday.imperialCeremonyDay
        |> isSomeOf EnthronmentOfEmperorReiwa
        |> should be True

    [<Fact>]
    let ``令和天皇の即位礼正殿の儀が行われた2019年10月22日は祝日である`` () =
        DateOnly(2019, 10, 22)
        |> JapaneseHoliday.imperialCeremonyDay
        |> isSomeOf EnthronmentCeremonyOfEmperorReiwa
        |> should be True

module SubstituteHolidayTests =
    [<Fact>]
    let ``日曜が祝日の場合は、次の月曜が振替休日である`` () =
        DateOnly(2021, 8, 9)
        |> JapaneseHoliday.substituteHoliday
        |> isSomeOf SubstituteDay
        |> should be True

    [<Fact>]
    let ``日曜の次に祝日が連続する場合は、祝日の次の平日が振替休日である`` () =
        DateOnly(2020, 5, 6)
        |> JapaneseHoliday.substituteHoliday
        |> isSomeOf SubstituteDay
        |> should be True

    [<Fact>]
    let ``祝日法の改正前は、日曜が祝日であっても次の月曜は振替休日ではない`` () =
        DateOnly(1973, 2, 12)
        |> JapaneseHoliday.substituteHoliday
        |> isSomeOf SubstituteDay
        |> should be False

module JapaneseWeekdayTests =
    [<Fact>]
    let ``年月日から変換できる`` () =
        DateOnly(2024, 1, 1) |> JapaneseWeekday.fromDateOnly |> should equal Getsu

    let namesOfWeekdays: obj[] list =
        [ [| "月"; Getsu |]
          [| "火"; Ka |]
          [| "水"; Sui |]
          [| "木"; Moku |]
          [| "金"; Kin |]
          [| "土"; Do |]
          [| "日"; Nichi |] ]

    [<Theory>]
    [<MemberData("namesOfWeekdays")>]
    let ``曜日名から変換できる`` (name: string) (expect: JapaneseWeekday) =
        JapaneseWeekday.fromName name |> isSomeOf expect |> should be True

    [<Theory>]
    [<MemberData("namesOfWeekdays")>]
    let ``曜日名に変換できる`` (expect: string) (weekday: JapaneseWeekday) =
        JapaneseWeekday.name weekday |> should equal expect

    [<Fact>]
    let ``曜日名でなければ変換できない`` () =
        JapaneseWeekday.fromName String.Empty |> should equal None

    let numbersOfWeekdays: obj[] list =
        [ [| 1; Getsu |]
          [| 2; Ka |]
          [| 3; Sui |]
          [| 4; Moku |]
          [| 5; Kin |]
          [| 6; Do |]
          [| 7; Nichi |] ]

    [<Theory>]
    [<MemberData("numbersOfWeekdays")>]
    let ``曜日の番号から変換できる`` (number: int) (expect: JapaneseWeekday) =
        JapaneseWeekday.fromNumber number |> isSomeOf expect |> should be True

    [<Theory>]
    [<MemberData("numbersOfWeekdays")>]
    let ``曜日の番号に変換できる`` (expect: int) (weekday: JapaneseWeekday) =
        JapaneseWeekday.number weekday |> should equal expect

    [<Fact>]
    let ``曜日の番号でなければ変換できない`` () =
        JapaneseWeekday.fromNumber 0 |> should equal None
