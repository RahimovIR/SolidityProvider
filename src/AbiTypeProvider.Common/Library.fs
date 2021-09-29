namespace AbiTypeProvider.Common

open System.Numerics
open Nethereum.Hex.HexTypes

[<AutoOpenAttribute>]
module misc =
    let inline runNow task =
        task
        |> Async.AwaitTask
        |> Async.RunSynchronously

    let inline bigInt (value: uint64) = BigInteger(value)
    let inline hexBigInt (value: uint64) = HexBigInteger(bigInt value)

    let (|IsIntOrUint|_|) (name: string) = 
        if not (System.String.IsNullOrWhiteSpace name)
            && (name.ToLower().StartsWith("uint") || name.ToLower().StartsWith("int")) then 
                Some name
        else
            None

    /// Convert solidity type to Type
    let solidityTypeToNetType solType = 
        match solType with
        | "uint8" -> typeof<uint8>
        | "uint16" -> typeof<uint16>
        | "uint32" -> typeof<uint32>
        | "uint64" -> typeof<uint64>
        | "int8" -> typeof<int8>
        | "int16" -> typeof<int16>
        | "int32" -> typeof<int32>
        | "int64" -> typeof<int64>
        | IsIntOrUint _ -> typeof<BigInteger>
        | "address" -> typeof<string>
        | "bool" -> typeof<bool>
        | "bytes" | "bytes32" | "bytes4" -> typeof<byte array>
        | _ -> typeof<string>
    

type GasLimit(v: HexBigInteger) = 
    member this.Value = v
    new (v:BigInteger) = GasLimit(HexBigInteger v)
    new (v:uint64) = GasLimit(bigint v)
    with override this.ToString() = v.ToString()
type GasPrice(v: HexBigInteger) = 
    member this.Value = v
    new (v:BigInteger) = GasPrice(HexBigInteger v)
    new (v:uint64) = GasPrice(bigint v)
    with override this.ToString() = v.ToString()
type WeiValue(v: HexBigInteger) = 
    member this.Value = v
    new (v:BigInteger) = WeiValue(HexBigInteger v)
    new (v:uint64) = WeiValue(bigint v)
    with override this.ToString() = v.ToString()

type gasLlimit = GasLimit
type gasPrice = GasPrice
type weiValue = WeiValue