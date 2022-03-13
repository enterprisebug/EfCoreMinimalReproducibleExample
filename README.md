# EfCoreMinimalReproducibleExample
Minimal reproducible example
 
## Steps to Reproduce

### Run with "F5".
Error that is thrown is:
![image](https://user-images.githubusercontent.com/1539741/158059006-4927b3bf-80d5-47f7-a6cd-20de48e4e9b3.png)

### Behavior when inspecting the variable in debug session 
if you set a debugger breakpoint in `Program.cs:16` and hover over `loaded.BaseEntity` you see:
![image](https://user-images.githubusercontent.com/1539741/158059020-ce8bcbc9-9a59-4caa-9e90-ce52b984703a.png)
if you then continue the execution no error occurs.
![image](https://user-images.githubusercontent.com/1539741/158059012-fd907b2e-f04e-481e-b784-bc593bf3e6b1.png)

### Interesting observation
if you uncomment `DemoDbContext.cs:114`, run with "F5" no error occurs
