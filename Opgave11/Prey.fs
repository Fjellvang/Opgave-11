// compile i mono ved: fsharpc -a Prey.fs -r Animals.dll

module Prey
open Animal

type Prey() = class
    inherit Animal()
end

