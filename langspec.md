# Declarative UH

Based on ASCII character set.

## Identifiers

### Numeric

A numeric is a float or an integer. The specific type is decided on the first assignment of a given variable. This is handled on compile time.

#### Floats

Writing the float assignment as a Regular Expression it would be `(numeric\s+)?([a-zA-Z]|_[a-zA-Z])[a-zA-Z0-9_]*(\s*?=\s*?(0|-?([1-9][0-9]*))\.([0-9]*))?;`

Bellow are some examples of assignment of floats.

```
numeric foo = 0.0;
numeric bar_2 = -1.4;
numeric _baz= 2.2;
numeric A = -110.9;
numeric b;

b = -43.2;
```

#### Integers

Writing the integer assignment as a Regular Expression it would be `(numeric\s+)?([a-zA-Z]|_[a-zA-Z])[a-zA-Z0-9_]*(\s*?=\s*?(0|-?([1-9][0-9]*)))?;`

Bellow are some examples of assignment of floats.

```
numeric foo = 0;
numeric bar_2 = -1;
numeric _baz= 2;
numeric A = -110;
numeric b;

b = 3;
```
#### String
a = "text"
#### Math expression
- +numeric
- -numeric
- \*numeric
- /numeric

#### Array
Writing the array declaration and initialization as a Regular Expression it would be
- a is numeric [5]
- a is string [5]
- a is bool [5]

Accessing array element:
- a@i

Multi-dim array
- a is numeric [5][5]
- a is string [5][5]
- a is bool [5][5]

Accessing array element:
- a@i@j

## Keywords
Here is a list of keywords
### Loops
Examples of "for" and "while"
#### For
```
for x in y
  \#Do stuff
end for

for x in 0 to 5
  \#Do stuff
end for

for x in 0 to 5
  \#Do stuff
  x is x + 1;
end for
```
#### While
```
while()
  \#< do stuff >#
end while
```
#### map
map is some double for loop thing or single

map(a,math expression)
```
for i in 0 to 4
  for j in 0 to 4
    a@i@j = 0;
  end for
end for
```

##Comments
####Single-line
\# insert text here
####Multi-line
\#< insert text here >#

##EBNF
