# favorite-mascot

##  The data

Heroku is holding an election to determine employees' favorite Salesforce mascot! There are 15 different mascots to choose from:

`Appy, Astro, Blaze, Bound, Brandy, Cloudy, Codey, Earnie, Einstein, Hootie, Koa, Max, Meta, SaaSy, Ruth`

Because the field of candidates is so big, Heroku electioneers have decided to use **rank-choice voting**. Each Heroku employee chooses up to 5 mascots (it can be fewer) on their ballot, ranking them in order of preference.

The following is an example ballot with `Koa` as the first choice, `Ruth` as the second choice, and so on:

```
{ "id": "1", "vote": ["Koa", "Ruth", "Appy", "Cloudy"] }
```

Notice how only **4** mascots are listed for `vote`. This is okay, but the max is **5** and **a mascot may only appear once in the list**.
	
## Analyzing the data

After voting is complete, the votes are tallied. These are the rules for counting:	

1. If the mascot with the most first-choice votes has **50% or more** of the total first-choice votes, counting completes and that mascot is named the winner.
1. If no mascot has **at least 15%** of the first-choice votes, the mascot that won **the most** first-choice votes is named the winner.
1. If neither **rule 1** nor **rule 2** result in a winner, a new round of counting begins:
    1. Any mascots that received **less than 15%** of the first-choice votes are eliminated. If a ballot’s first-choice mascot is eliminated, that vote now counts towards the new first-choice mascot of the remaining uneliminated candidates on the ballot.
    1. Rounds of counting repeat in this way until a mascot reaches **50% or more** of the first-choice votes in the current round **OR** no mascot has **at least 15%** of the first-choice votes in the current round.

**You may assume that any ballot data we provide you will result in a winner when counted according to these rules.** In other words, it is impossible to end up in an infinite loop.

### Example

Let's run through an example to demonstrate how the counting works. Suppose only 8 ballots were cast in our election:

```
[{ "id": "1", "vote": ["Koa", "Ruth", "Appy", "Cloudy"] },
 { "id": "2", "vote": ["Appy", "Meta", "Astro"] },
 { "id": "3", "vote": ["Ruth", "Koa", "Astro"] },
 { "id": "4", "vote": ["Koa", "Brandy", "Bound", "Ruth"] },
 { "id": "5", "vote": ["Blaze", "Earnie", "Einstein"] },
 { "id": "6", "vote": ["Hootie", "Codey", "Koa", "Brandy", "Max"] },
 { "id": "7", "vote": ["Blaze", "Earnie", "Hootie"] },
 { "id": "8", "vote": ["Blaze", "Koa", "Astro", "Brandy", "Bound"] }]
```

To begin, we just look at each ballot's first choice. That gives these results:
```
Koa:    25.0%
Appy:   12.5%
Ruth:   12.5%
Hootie: 12.5%
Blaze:  37.5%
```
and everyone else got `0%`. Based on the counting rules, all candidates except Koa and Blaze are now eliminated.

In the second round of counting, the ballots now look like this:

```
[{ "id": "1", vote: ["Koa"] },
 { "id": "2", vote: [] },
 { "id": "3", vote: ["Koa"] },
 { "id": "4", vote: ["Koa"] },
 { "id": "5", vote: ["Blaze"] },
 { "id": "6", vote: ["Koa"] },
 { "id": "7", vote: ["Blaze"] },
 { "id": "8", vote: ["Blaze", "Koa"] }]
```
and the results are:
```
Koa:   57%
Blaze: 43%
```

Thus, **Koa is the winner**.
Notice how ballot `2` is not counted in the second round because all its candidates were eliminated.


## Your task
	
Your task is to write a program that will determine the winner of the election and write that winner to standard output. The votes cast in the election are in the `input.json` file. The input consists of a valid JSON array in which each element is a ballot. In your submission, please include:
- Your code.
- Brief instructions on how to run your program (e.g. `cat input.json | go run count_votes.go`).
- Details of any system-level requirements or dependencies needed to run your program.
- A screenshot of the output when running the program for the `input.json` data.

### Examples

To help you test your code, we have provided you with a set of example inputs in the `examples` folder. The winners for each of these example elections is listed in `examples/answers.md`.
