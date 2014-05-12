yasm -f elf fizzbuzz.asm
gcc -o fizzbuzz fizzbuzz.o driver.c asm_io.o
./fizzbuzz
