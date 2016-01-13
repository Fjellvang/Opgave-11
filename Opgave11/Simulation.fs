// Compile i mono ved: fsharpc Simpulation.fs -r Animals.dll -r Prey.dll -r Predator.dll

module Simulation
open Predator
open Prey
open Island

// FORSKELLIGE TESTS.
let island = new Island()
let mutable running = true
while running do
    island.DrawMap()
    for prey in island.PreyArray do
        try
            prey.Move(island.getFreeSpot(prey))
        with
            | :? System.Exception -> ignore()
    for predator in island.PredatorArray do
        try
            predator.Move(island.getFreeSpot(predator))
        with
            | :? System.Exception -> ignore()
    let x = System.Console.ReadLine()
    if x = "1" then running <- true
    //System.Console.Clear()
    