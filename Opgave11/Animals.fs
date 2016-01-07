module Animal
[<AbstractClass>]
type Animal() = class
    let mutable x = 0
    let mutable y = 0    
    member this.posistion with get() = (x,y)
                           and set(v) = x <- fst v
                                        y <- snd v
    abstract member Move : int * int -> unit
    default this.Move(x,y) = this.posistion <- (x,y)
    abstract member breedTime : float
    //TODO, Find ud af hvordan mappen skal stores så move kan laves.
end

