module Prey


// Compile modul i mono ved: fsharpc -a Animals.fs
type posistion = {x:int; y:int}
let rnd = new System.Random()

type Prey(p:int*int) = class
    (*let mutable x = fst p
    let mutable y = snd p*)
    let mutable pos = { x=fst p;y=snd p}
    let mutable breedtime = 5    
    // x,y posistion af dyr. gemt i tuple. afhænger af hvordan vi gemmer dyrerne. 
    // derved kan vi evt tjekke om deres x og y posistion er den samme og derfra evaluere hvad der skal ske
    member this.posistion with get() = pos
                           and set(v) = pos <- v
    // Move metoden. Skal, måske, rettes
    //abstract member Move : unit -> unit
    member this.getNewPosistion() =
        let direction = function
        | 0 -> {x=this.posistion.x+1;y=0} //øst
        | 1 -> {x=0;y=this.posistion.y+1} // nord
        | 2 -> {x=(this.posistion.x-1);y=0} // west
        | 3 -> {x=0;y=(this.posistion.y-1)} // syd
        | _ -> failwith "WRONG ARGUMENT"
        
        direction(rnd.Next(0,4))
    // repopulations tid for dyrerne. lavet som abstract i tilfæde af at prey og predator's tid ikke er ens
    // Skal angives ved programstart - ingen sikkerhed om det skal være individuelt eller ej.
    abstract member breedTime : int with get, set
    default this.breedTime with get() = breedtime
                           and set(v) = breedtime <- v // TEMP variable
    abstract member Tick : unit -> unit // funktion til at decrementere breedtime osv. Skal overrides i Predator, bruge base og så tilføje predator specifik kode. 
    default this.Tick() =
        breedtime <- breedtime-1
    
end


