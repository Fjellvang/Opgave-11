// Compile Modul i Mono ved: fsharpc -a Predator.fs -r Animals.dll

module Predator
open Prey

type Predator(p:int*int) = class
    inherit Prey(p)
    // fixed starve time
    let mutable starvetime = 5.0
    member this.starveTime with get() = starvetime
                           and set(v) = starvetime <- v
end


