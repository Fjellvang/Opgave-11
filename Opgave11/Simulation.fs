// Compile i mono ved: fsharpc Simpulation.fs -r Animals.dll -r Prey.dll -r Predator.dll

module Simulation
open Predator
open Prey
open Animal

let x = Predator() :> Animal

printf "%b" (x :? Predator)
ignore(System.Console.ReadLine())
