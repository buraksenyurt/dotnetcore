using MessagePack;
using ProtoBuf;

[MessagePackObject, ProtoContract]
public class Book{
    [Key(0),ProtoMember(1)]
    public int Id { get; set; }
    [Key(1),ProtoMember(2)]
    public string Title { get; set; }
    [Key(2),ProtoMember(3)]
    public double Price { get; set; }
}