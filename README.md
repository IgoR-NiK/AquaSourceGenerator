# Benchmark

Benchmark using BenchmarkDotNet

|                      Method |       Mean |     Error |     StdDev |     Median |
|---------------------------- |-----------:|----------:|-----------:|-----------:|
|          ManualRegistration |   2.589 ms | 0.1024 ms |  0.2905 ms |   2.490 ms |
| SourceGeneratorRegistration |   2.472 ms | 0.0644 ms |  0.1837 ms |   2.438 ms |
|      ReflectionRegistration | 184.947 ms | 7.0676 ms | 20.6166 ms | 178.803 ms |


# How to Debug Source Code Generators

At the beginning of the method 'Initialize' should insert the next code 

```sc
    public void Initialize(GeneratorInitializationContext context)
    {
#if DEBUG
        if (!Debugger.IsAttached)
        {
            Debugger.Launch();
        }

        Debug.WriteLine("Initalize code generator");
#endif         
        
        //   ...  ...  ...
        
    }
```

After this if we force a rebuild of our Generation library, we’ll see a prompt asking to specify where the code should be debugged.

From this dialog you can either select the existing instance of Visual Studio, or open a new instance. My preference it to open up a new instance of Visual Studio – it feels a bit to much like inception to debug the code generation in the same instance of Visual Studio but this comes down to whatever works for you.

Once the debugger is attached, you can step through the code, view variables, set breakpoints etc. Unfortunately it appears that Edit and Continue doesn’t work, so it does mean that in order to make changes you need to stop debugging, make changes and then rebuild the project in order to trigger the code generator to run.
