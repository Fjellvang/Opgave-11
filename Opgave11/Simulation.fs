// Compile i mono ved: fsharpc Simpulation.fs -r Animals.dll -r Prey.dll -r Predator.dll

module Simulation
open Predator
open Prey


let x = Predator() :> Prey
// giver true.
printf "%b" (x :? Predator)
ignore(System.Console.ReadLine())
