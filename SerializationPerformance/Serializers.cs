using System.Collections.Generic;
using System.IO;
using System.Threading;
using BenchmarkDotNet.Attributes;
using MessagePack;
using Newtonsoft.Json;
using ProtoBuf;

[MarkdownExporter, AsciiDocExporter, HtmlExporter, CsvExporter, RPlotExporter, CoreJob, MaxWarmupCount(8),MinIterationCount(3), MaxIterationCount(5)] // DryCoreJob
public class Serializers
{
    [Params(1,10,1000,10000,100000)]
    public int BookCount { get; set; }
    public List<Book> books = new List<Book>();
    private string rootPath="c:\\projects\\data\\";

    [GlobalSetup]
    public void LoadDataset()
    {
        for (int i = 1; i < BookCount; i++)
        {
            books.Add(new Book
            {
                Id = i,
                Title = $"Book_{i}",
                Price = 10
            });
        }
    }

    [Benchmark]
    public void ToJson()
    {
        var result = JsonConvert.SerializeObject(books);
        WriteToFile($"json_sample_{BookCount}.json",result);
    }
    [Benchmark]
    public void ToMessagePack()
    {
        var result = MessagePackSerializer.Serialize(books);
        WriteToFile($"mPack_sample_{BookCount}.bin",result);
    }

    [Benchmark]
    public void ToMessagePackJson()
    {
        var content = MessagePackSerializer.Serialize(books);
        var result = MessagePackSerializer.ToJson(content);
        WriteToFile($"mPack_Json_sample_{BookCount}.bin",result);
    }

    [Benchmark]
    public void ToProtobuf()
    {
        using (FileStream fs = new FileStream($"{rootPath}protobuf_sample_{BookCount}.bin", FileMode.Create))
        {
            Serializer.Serialize(fs, books);
        }
    }

    public void WriteToFile(string fileName,string content)
    {
        using (FileStream fs = new FileStream(Path.Combine(rootPath,fileName), FileMode.Create))
        {
            using (StreamWriter writer = new StreamWriter(fs))
            {
                writer.Write(content);
            }
        }
    }

    public void WriteToFile(string fileName,byte[] content)
    {
        using (FileStream fs = new FileStream(Path.Combine(rootPath,fileName), FileMode.Create))
        {
            using (StreamWriter writer = new StreamWriter(fs))
            {
                writer.Write(content);
            }
        }
    }
}