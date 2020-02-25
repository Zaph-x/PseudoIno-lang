# S

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
