module Island
open Prey
open Predator
// Måske en klasse der holder info om en plads er optaget af et dyr.
type Island() = class
    let preyArray = [|new Prey((0,0));new Prey(1,0);new Prey(0,1)|]
    let predatorArray = [|new Predator((10,10))|]
    let mutable islandx = 10
    let mutable islandy = 10
    // metode til at sætte ø størelsen i tilfælde af et 10,10 map er for småt
    member this.setIslandSize with set(v) =
                                        islandx <- fst v
                                        islandy <- snd v
    // finde en måde at sætte prey's posistion til deres elements plads i øen ?
    member this.PreyArray with get() = preyArray
    member this.PredatorArray with get() = predatorArray
    
    member this.DrawMap() =
        //en metode der tegner NxN map, måske 10x10
        //hvis der findes en prey / predator med x,y coordinater ved i,j gennemløb
        // tegn da en W for ulv, M for moose
        for i=0 to islandx do
            for j=0 to islandy do
                printf "%s " (this.AnimalSign({x=i;y=j}))
            System.Console.WriteLine()
    // print en map med _ for empty W for ulv, M for Moose.
    member private this.AnimalSign(X) =
        
        // TODO: RYD OP I HJÆLPE FUNKTIONER.
        // Måske find en bedre måde at fixe de der lamda expressions.
        let mutable sign = "_"
        // hjælpe funktion. returnerer true hvis et dyrs posistion passer med en givent tuple
        
        // doven
        //let psfnkt(p:Predator) = p.posistion = {x=X;y=Y}
        // endnu en hjælpe funktion
        
        if Array.exists (fun (ele:Prey) -> ele.posistion = X) preyArray then sign <- "M"
        else if Array.exists (fun (ele:Predator) -> ele.posistion = X) predatorArray then sign <- "W"
        
        sign
    
    member this.MovePrey(A:Prey[]) =
        let h(x,y) = Array.exists(fun (ele:Prey) -> ele.posistion = x) y
        for p in A do
            let newPos = p.getNewPosistion()
            if not(h(newPos,A)) then 
                p.posistion <- newPos
    
    // returner free posision
    member this.getFreeSpot(that:Prey) =
        // funktion der returnerer en ledig plads ved siden af x's x,y koordinater kan fødes til Move evt?
        // må ikke overstive +/- islandx/islandy og skal maks være +1 af preys koordinater.
        let currentX = that.posistion.x
        let currentY = that.posistion.y

        // midlertidig løsning. hvis der ikke findes et ordenligt tal så må den smide exception
        let mutable newPos = {x=islandx+1; y=islandy+1}
        
        // funktioner: er x inside bounds ?
        let f x = if ((x <= islandx) && (x >= 0)) then true else false
        let g y = if ((y <= islandy) && (y >= 0)) then true else false
        // returnerer true hvis x pos findes
        let h x = Array.exists(fun (ele:Prey) -> ele.posistion = x) preyArray
        
        if not(h {x=currentX+1;y=currentY}) && (f (currentX+1) && g currentY) then
            newPos <- {x=currentX+1;y=currentY}

        else if not(h {x=currentX+1;y=currentY-1}) && (f (currentX+1) && g (currentY-1)) then
            newPos <- {x=currentX+1;y=currentY-1}

        else if not(h {x=currentX;y=currentY-1}) && (f currentX && g (currentY-1)) then
            newPos <- {x=currentX;y=currentY-1}

        else if not(h {x=currentX;y=currentY+1}) && (f currentX && g (currentY+1)) then
            newPos <- {x=currentX;y=currentY+1}

        else if not(h {x=currentX-1;y=currentY}) && (f (currentX-1) && g currentY) then
            newPos <- {x=currentX-1;y=currentY}

        else if not(h {x=currentX-1;y=currentY+1}) && (f (currentX-1) && g (currentY+1)) then
            newPos <- {x=currentX-1;y=currentY+1}

        else if not(h {x=currentX-1;y=currentY-1}) && (f (currentX-1) && g (currentY-1)) then
            newPos <- {x=currentX-1;y=currentY-1}
        else
            failwith "Out of bounds!"
        
        newPos
    member this.getFreeSpot(that:Predator) =
        // overloaded metode. Caster bare Predator til Prey og kører samme show. sygt nice
        this.getFreeSpot(that:>Prey)
end