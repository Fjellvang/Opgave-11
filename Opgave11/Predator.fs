// Compile Modul i Mono ved: fsharpc -a Predator.fs -r Animals.dll
module Predator
open Prey

type Predator(p:int*int) = class
    inherit Prey(p)
    // fixed starve time skal angives ved programstart
    let mutable starvetime = 5
    member this.starveTime with get() = starvetime
                           and set(v) = starvetime <- v
    override this.Tick() =
        base.Tick()
        starvetime <- starvetime-1
end


