

# wasp

##What A Simple Piece of language for web assembly

##Introduction

###Disclaimer

This is a project intended for personal learning, a narrow toolset and, since the web assembly specs are evolving as we speak, one thing seems sure : Things will break



### Motivation

I heard about web assembly mid august 2017 and my first thought was to migrate an existing spare time 3D engine written in TypeScript and learn C++ in the process. 2 weeks later I began to realize the state of matter as a even a minimal js lib would explode in size after a non trivial compiling process. After reading loads of broken tutorials I finally ran over [Mads Sejersens blog post][MS] and realized what web assembly actually is and more important what it is not. So why not build a simple number crunching language that maps to the web assembly specifications an avoid the clutter. How hard can it be? Right! 



### Current Goal

An inferior language that compiles directly to web assembly

* Global functions
* Basic Math operations
* Import / Export declarations
* Auto generate java script glue code



## Current Target Specifications

### Types

```
int
int2
dec
dec2
```

### Comments

```
// comment
```

### Keywords & Identifiers

```
export
```

### Literals

```
[a-z] descriptors
```

### Punctuators

```
()
```

### Operators

```
+
=>
```



### Current target implementation

```
export
(
	int Add (int x, int y) => x + y
)

```



## Specification

### There is absolutely nothing here



[MS]: https://medium.com/@MadsSejersen/webassembly-the-missing-tutorial-95f8580b08ba
