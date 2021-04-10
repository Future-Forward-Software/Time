Recursive functions usually create a new stack frame for each recursion call, meaning that for long running recursion you can have a stack overflow.

Tail call recursion is an optimisation strategy that involves reusing the stack frame (think a goto statement) instead of creating a new frame when a function calls itself by returning the value immediately, instead of the new frame added which would need to be popped off to get the result.
This means O(1) stack growth.

Unfortunately the CLR does not support tail call optimisation for C#.

Trampolining is a strategy to overcome this problem, albeit much slower. Trampolining involves either returning the final result or returning a new function, but not actually returning the result of that function.
This means lazy evaluation. Instead of creating a new stack frame by eagerly calling the function, by only returning a reference to the function we can transform a recursive algorithm into an interative one which doesnt create new stack frames.

eg 
while(ShouldRecurse)
	recurse();

vs
recurse();
	recurse();
		recurse();
			recurse();
				recurse();
					recurse();
						recurse();
							recurse();


https://thomaslevesque.com/2011/09/02/tail-recursion-in-c/
http://blog.functionalfun.net/2008/04/bouncing-on-your-tail.html
https://marmelab.com/blog/2018/02/12/understanding-recursion.html

Usage:

        public void ClientCode()
        {
            int result = Trampoline.Execute(() => MyLongRecursiveMethod(100));
        }

        public Bounce<int> MyLongRecursiveMethod(int significantInput)
        {
            int calculation = ...
            bool hasCalculatedFinalResult = ...

            if (hasCalculatedFinalResult)
                return Trampoline.Return(calculation);
            return Trampoline.Bounce(() => MyLongRecursive(calculation));
        }