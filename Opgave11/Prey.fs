module Prey
// Compile modul i mono ved: fsharpc -a Animals.fs

type Prey(p:int*int) = class
    let mutable x = fst p
    let mutable y = snd p
    let mutable breedtime = 5.0    
    // x,y posistion af dyr. gemt i tuple. afhænger af hvordan vi gemmer dyrerne. 
    // derved kan vi evt tjekke om deres x og y posistion er den samme og derfra evaluere hvad der skal ske
    member this.posistion with get() = (x,y)
                           and set(v) = x <- fst v
                                        y <- snd v
    // Move metoden. Skal, måske, rettes
    abstract member Move : int * int -> unit
    default this.Move(x,y) = this.posistion <- (x,y)
    // repopulations tid for dyrerne. lavet som abstract i tilfæde af at prey og predator's tid ikke er ens
    // Skal angives ved programstart - ingen sikkerhed om det skal være individuelt eller ej.
    abstract member breedTime : float with get, set
    default this.breedTime with get() = breedtime
                           and set(v) = breedtime <- v // TEMP variable
    //TODO, Find ud af hvordan mappen skal stores så move kan laves.
end

