# Welcome to the Share Price Calculor#

## Overview

All of the use cases and bonuses are in this implementation and it appears to be functioning correctly. 
Application was broken out into functional layers using VS projects and IoC was implemented using Ninject.

All business logic is handled by services except in the cases of where the models do their own calculations. 

Linq was used to simplify data queries in the code. 

Unit tests are in place for most of the complex business logic areas and are all passing and green.

## How the data works

Input will come in via stdin in the formats that were provided in the example. It will handle all the use cases.

Output is piped to the stdout.

I tested it by running SharePriceCalculator.exe < someinputfile.txt
 
## Building and Running

Build the console app either in VisualStudio or using MSBuild. Dev mode should work fine. 

Application should run, but there are some exceptions I throw if there is bad data coming in the stdin channel. 

## 3rd party libraries used

Ninject

CsvHelper


