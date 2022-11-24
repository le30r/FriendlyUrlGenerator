# Friendly URL Generator

## Usage

Generates readable and easy to remember combinations of words to use as page names.

    UrlGenerator generator = new(Locale.EN);
    generator.Generate();

Returns a combination of the pattern "verb-adjective-noun" on English.

Locale has the following identifiers:

    Locale.EN // on English
    Locale.RU // on Russian


It is possible to change the pattern of the displayed URL:

    UrlGenerator generator = new(Locale.EN);
    generator.Generate("^v-^a-^n");

Does the same as the code above.

## Pattern syntax:

| Symbol                | Meaning                                                                                       |
|:----------------------|-----------------------------------------------------------------------------------------------|
| ^                     | service, placed before one of the letters                                                     |
| ^v                    | verb                                                                                          |
| ^n                    | noun                                                                                          |
| ^a                    | adjective                                                                                     |
| ^V                    | Capitalized Verb                                                                              |
| ^N                    | Capitalized Noun                                                                              |
| ^A                    | Capitalized Adjective                                                                         |
| any letter <br/>(except ^) | without the ^ any character is used as usual, allowing for example to add delimiters |





