# Koyomi.FSharp

"Koyomi.FSharp" is a thin wrapper for `System.DateTime`.
This can determine Japanese holidays and no configuration file or database maintenance is required.

# Requirement

- `dotnet >= 7.0`

# Installation

Add `Koyomi.FSharp` as a dependency in your `*.fsproj`.

``` shell
dotnet add Koyomi.FSharp
```

# Usage

## As a wrapper of `System.DateTime`

To use it like `System.DateTime`, do the following.

``` fsharp
open System
open Koyomi.FSharp

let k = DateTime(2023, 12, 1) |> Koyomi.from
Koyomi.year k       // 2023
Koyomi.month k      // 12
Koyomi.date k       // 1
Koyomi.dayOfWeek k  // DayOfWeek.Sunday

Koyomi.addYears 1 k   // 2024-01-01
Koyomi.addMonths 1 k  // 2023-02-01
Koyomi.addDays 1 k    // 2023-01-02

Koyomi.format "yyyy/MM/dd" k  // 2023/01/01
```

## Era(Japanese)

To derive the Japanese calendar, do the following.

``` fsharp
open System
open Koyomi.FSharp

let e = DateTime(2023, 12, 1) |> Era.from
Era.name e         // 平成
Era.year e         // 5
Era.nameAndYear e  // 平成5年
Era.epoc e         // Reiwa(Discriminated union type)
```

## Japanese holidays

To derive the Japanese holidays, do the following.

``` fsharp
open System
open Koyomi.FSharp

let k = DateTime(2023, 1, 1) |> Koyomi.from
Koyomi.holiday k    // Some "元日"
Koyomi.isHoliday k  // true

let k = DateTime(2023, 1, 10) |> Koyomi.from
Koyomi.holiday k    // None
Koyomi.isHoliday k  // false
```

## Japanese calendar

To use it as a Japanese calendar, do the following.

``` fsharp
open System
open Koyomi.FSharp

// From the specified date to today.(Include specified date)
DateTime.Now.AddDays(-10) |> Calendar.from

// From today until the specified date.(Include specified date)
DateTime.Now.AddDays(10) |> Calendar.until

// Specifiy a range.
let from = DateTime.Now.AddDays(-10)
let until = DateTime.Not.AddDays(10)
Calendar.between from until

// An inconsistent range will return an empty list.
Calendar.between until from |> List.isEmpty  // true
```

You can also generate a calendar by specifying the year, month, or year.

``` fsharp
open Koyomi.FSharp

// Specified year.
Calendar.ofYear DateTime.Now.Year

// Specified year and month.
Calendar.ofMonth DateTime.Now.Year DateTime.Now.Month
```

If you need only holidays, use `ofHolidays`.

``` fsharp
open System
open Koyomi.FSharp

DateTime.Now.Year |> Calendar.ofYear |> Calendar.ofHolidays
```

# Note

Koyomi.FSharp only handles dates. Hours, minutes, and seconds are not handled.

# Author

[panther-king](https://github.com/panther-king)

# License

"Koyomi.FSharp" is under [MIT licesnes](https://en.wikipedia.org/wiki/MIT_License).
