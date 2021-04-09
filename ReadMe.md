# Time
This package abstracts dependencies on dates and times.

## DateTime abstraction
### Why?
Writing testable code is often difficult when using DateTime directly. We can see that in this code example for creating a reservation.

```public ActionResult Post(ReservationDto dto)
{
    if (!DateTime.TryParse(dto.Date, out var _))
        return BadRequest($"Invalid date: {dto.Date}.");
 
    Reservation reservation = Mapper.Map(dto);
 
    if (reservation.Date < DateTime.Now))
        return BadRequest($"Invalid date: {reservation.Date}.");
 
    var reservations = Repository.ReadReservations(reservation.Date);
    bool accepted = maîtreD.CanAccept(reservations, reservation);
    if (!accepted)
        return StatusCode(StatusCodes.Status500InternalServerError, "Couldn't accept.");
 
    Repository.Create(reservation);
    return Ok();
}```
https://blog.ploeh.dk/2020/04/06/repeatable-execution-in-c/

There is business logic here which relies on comparrison of dates, so to test this code you would need to set the reservation date to before DateTime.Now in one test, and after in another.
This isnt good though because your test will now be slightly different every time it runs. You also dont have good control over what the DateTime values are and if you need high precision you could face race conditions in your tests - unlikely though that is.

Domain Driven Design also teaches us that infrastructure concerns should not be in your domain model, and accessing the system time is infrastructure - you should decide for yourself if this is a good reason for abstracting Time as whilst I agree with this, I can see that it borders on dogmatic and you could lose sight of the point that way.

See https://enterprisecraftsmanship.com/posts/domain-model-purity-current-time/ for a more in-depth breakdown.
### How?

## Timer abstraction
### Why?
Abstracting timers for testing is what gave birth to this project originally. Given a common problem like a timed hosted service (https://docs.microsoft.com/en-us/aspnet/core/fundamentals/host/hosted-services?view=aspnetcore-5.0&tabs=visual-studio#timed-background-tasks) that uses a timer, testing this can be a nightmare.

You might go straight to a normal Timer dependency like above and use Task.Delay in your test - you will hit the problem that Timer is not very precise when testing. Your timer might execute once too many or once too little because of this imprecision, so to deal with this inprecision you just make your timer longer via depency injection and your Task.Delays longer to make the imprecision time smaller. This will work in most cases but the instability in your tests will still be there and will inevitably come back to bite you, or your tests will just be slow and with lots of tests it can make life difficult.

Your next step might be something simple like this which would be more stable:

```public class MyTimer : IMyTimer {
	private readonly _msToWait;
	private readonly _action;
	
    MyTimer(int msToWait, Action action) {
	_msToWait = msToWait;
	_action = action;
	}
	
	private void BeginTimer() {
		Task.Delay()msToWait).GetAwaiter().GetResult();
		_action();
		BeginTimer();
	}
}```

This would probably work but be careful as C# does not support tail call recursion optimisation (the stack is not cleaned when a recursive call is made) so this could lead to a stackoverflow exception.

Given the amount of pitfalls here it would be best to use this package instead for confidence.
### How?
