module Island
open Prey
open Predator
// Måske en klasse der holder info om en plads er optaget af et dyr.
type Island() = class
    let preyArray = [|new Prey((0,0))|]
    let predatorArray = [|new Predator((10,10))|]
    let mutable islandx = 10
    let mutable islandy = 10
    member this.setIslandSize with set(v) =
                                        islandx <- fst v
                                        islandy <- snd v
    // finde en måde at sætte prey's posistion til deres elements plads i øen ?
    
    member this.DrawMap() =
        //en metode der tegner NxN map, måske 10x10
        //hvis der findes en prey / predator med x,y coordinater ved i,j gennemløb
        // tegn da en W for ulv, M for moose
        for i=0 to islandx do
            for j=0 to islandy do
                printf "%s " (this.AnimalSign(i,j))
            System.Console.WriteLine()
    // print en map med _ for empty W for ulv, M for Moose.
    member private this.AnimalSign(x,y) =
        
        // TODO: RYD OP I HJÆLPE FUNKTIONER.
        // Måske find en bedre måde at fixe de der lamda expressions.
        let mutable sign = "_"
        // hjælpe funktion. returnerer true hvis et dyrs posistion passer med en givent tuple
        let posFnkt(p:Prey) = p.posistion = (x,y)
        // doven
        let psfnkt(p:Predator) = p.posistion = (x,y)
        // endnu en hjælpe funktion
        
        if Array.exists posFnkt preyArray then sign <- "M"
        else if Array.exists psfnkt predatorArray then sign <- "W"
        
        sign
    
end