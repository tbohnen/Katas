yasm -f elf test.asm
gcc -o test test.o driver.c asm_io.o
./test
