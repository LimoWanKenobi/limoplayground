namespace FsKarel.Core

type KarelWorld = {
    k_x: int;
    k_y: int
} with
    member this.add() = this.k_x + this.k_y
