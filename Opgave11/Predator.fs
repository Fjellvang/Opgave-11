// Compile Modul i Mono ved: fsharpc -a Predator.fs -r Prey.dll
module Predator
open Prey

type Predator(p:int*int) = class
    inherit Prey(p)
    // fixed starve time skal angives ved programstart
    static let mutable starve = 5
    let mutable starvetime = Predator.starving
    static member starving with get() = starve
                            and set(v) = starve <- v
    member this.starveTime with get() = starvetime
                           and set(v) = starvetime <- v
    member this.resetStarveTime() =
        this.starveTime <- Predator.starving+1
    override this.Tick() =
        base.Tick()
        starvetime <- starvetime-1
end


