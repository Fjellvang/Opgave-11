﻿// Compile i mono ved: fsharpc Simpulation.fs -r Animals.dll -r Prey.dll -r Predator.dll

module Simulation
open Predator
open Prey
open Island

// FORSKELLIGE TESTS.
let island = new Island()
let mutable running = true
printfn "Please specify Breedtime for all animals: (1-9999)"
let x = System.Console.ReadLine()
Prey.breeding <- int x
printfn "Please specify Starvetime for Predators: (1-9999)"
let y = System.Console.ReadLine()
Predator.starving <- int y
while running do
    island.DrawMap()
    island.MovePrey()
    island.MovePredator()
    for x in island.PreyArray do
        island.breedPrey(x)
        x.Tick()
    for x in island.PredatorArray do
        island.breedPredator(x)
        island.starve(x)
        island.eat(x)
        x.Tick()
    (*for prey in island.PreyArray do
        try
            prey.Move(island.getFreeSpot(prey))
        with
            | :? System.Exception -> ignore()
    for predator in island.PredatorArray do
        try
            predator.Move(island.getFreeSpot(predator))
        with
            | :? System.Exception -> ignore()*)
    let x = System.Console.ReadLine()
    if x = "1" then running <- true
    //System.Console.Clear()
    