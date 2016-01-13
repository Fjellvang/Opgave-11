module Island
open Prey
open Predator
// Måske en klasse der holder info om en plads er optaget af et dyr.
type Island() = class
    let mutable preyArray = [|new Prey((0,0));new Prey(1,0);new Prey(0,1);new Prey((2,2))|]
    let mutable predatorArray = [|new Predator((10,10));new Predator((5,5));new Predator((8,8));new Predator((9,10));new Predator((10,9))|]
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
        printfn "Wolves: %i, Moose: %i" predatorArray.Length preyArray.Length
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
    
    member this.MovePrey() =
        let h(x,y) = Array.exists(fun (ele:Prey) -> ele.posistion = x) y
        // TEST in bounds
        let f(x,y) = if ((x <= islandx) && (x >= 0)) && ((y <= islandy) && (y >= 0)) then true else false
        for p in preyArray do
            let newPos = p.getNewPosistion()
            if not(h(newPos,preyArray)) && f(newPos.x,newPos.y) then 
                p.posistion <- newPos
    
    member this.MovePredator() =
        let h(x,y) = Array.exists(fun (ele:Predator) -> ele.posistion = x) y
        // TEST in bounds
        let f(x,y) = if ((x <= islandx) && (x >= 0)) && ((y <= islandy) && (y >= 0)) then true else false
        for p in predatorArray do
            let newPos = p.getNewPosistion()
            if not(h(newPos,predatorArray)) && f(newPos.x,newPos.y) then 
                p.posistion <- newPos
   
    (*member this.breed(that:Prey) =
        if (that.breedTime = 0) then
            let newPos = this.getFreeSpot(that)
            *)
            (*if newPos.IsSome then
                that.ResetBreedtime()
                let newnew = newPos.Value
                preyArray <- Array.append preyArray [|new Prey(newnew.x,newnew.y)|]*)
    member this.breedPrey(that:Prey) =
        if that.breedTime <= 0 then
            let Q = this.getFreeSpot(that)
            if not(Q = None) then
                that.ResetBreedtime()
                let newPos = Q.Value
                preyArray <- Array.append preyArray [|new Prey(newPos.x,newPos.y)|]

    member this.breedPredator(that:Predator) =
        if that.breedTime <= 0 then
            let Q = this.getFreeSpot(that)
            if not(Q = None) then
                that.ResetBreedtime()
                let newPos = Q.Value
                predatorArray <- Array.append predatorArray [|new Predator(newPos.x,newPos.y)|]

    

    member this.starve(that:Predator) =
        if (that.starveTime = 0) then 
            predatorArray <- predatorArray |> Array.filter (fun x -> x.posistion <> that.posistion)

    member this.eat(that:Predator) =
        let x = preyArray.Length
        preyArray <- preyArray |> Array.filter (fun x -> x.posistion <> that.posistion)
        if x > preyArray.Length then
            that.resetStarveTime()
    
    // returner free posision
    member this.getFreeSpot(that:Prey) =
        // funktion der returnerer en ledig plads ved siden af x's x,y koordinater kan fødes til Move evt?
        // må ikke overstive +/- islandx/islandy og skal maks være +1 af preys koordinater.
        let currentX = that.posistion.x
        let currentY = that.posistion.y

        // midlertidig løsning. hvis der ikke findes et ordenligt tal så må den smide exception
        let mutable newPos = Some {x=islandx+1; y=islandy+1}
        
        // funktioner: er x inside bounds ?
        let f x = if ((x <= islandx) && (x >= 0)) then true else false
        let g y = if ((y <= islandy) && (y >= 0)) then true else false
        // returnerer true hvis x pos findes
        let h x = Array.exists(fun (ele:Prey) -> ele.posistion = x) preyArray
        
        if not(h {x=currentX+1;y=currentY}) && (f (currentX+1) && g currentY) then
            newPos <- Some {x=currentX+1;y=currentY}

        else if not(h {x=currentX+1;y=currentY-1}) && (f (currentX+1) && g (currentY-1)) then
            newPos <- Some {x=currentX+1;y=currentY-1}

        else if not(h {x=currentX;y=currentY-1}) && (f currentX && g (currentY-1)) then
            newPos <- Some {x=currentX;y=currentY-1}

        else if not(h {x=currentX;y=currentY+1}) && (f currentX && g (currentY+1)) then
            newPos <- Some {x=currentX;y=currentY+1}

        else if not(h {x=currentX-1;y=currentY}) && (f (currentX-1) && g currentY) then
            newPos <- Some {x=currentX-1;y=currentY}

        else if not(h {x=currentX-1;y=currentY+1}) && (f (currentX-1) && g (currentY+1)) then
            newPos <- Some {x=currentX-1;y=currentY+1}

        else if not(h {x=currentX-1;y=currentY-1}) && (f (currentX-1) && g (currentY-1)) then
            newPos <- Some {x=currentX-1;y=currentY-1}
        else
            newPos <- None
        
        newPos
    member this.getFreeSpot(that:Predator) =
        // overloaded metode. Caster bare Predator til Prey og kører samme show. sygt nice
        this.getFreeSpot(that:>Prey)
end