

;
; file: math.asm
; This program demonstrates how the integer multiplication and division
; instructions work.
;
; To create executable:
; nasm -f coff math.asm
; gcc -o math math.o driver.c asm_io.o

%include "asm_io.inc"

segment .data
;
; Output strings
;
prompt1 dd "Please enter your number: ", 0
value1 db 3
value2 db 5
value3 db 0
buzzValue   db    "Buzz", 0
fizzValue   db    "Fizz", 0
value4 db 0
segment .bss
input   resd 1


segment .text
        global  asm_main
asm_main:
        enter   0,0               ; setup routine
        pusha

        mov    eax, prompt1      ; print out prompt
        call   print_string

        mov    eax, 0
        call   read_int          ; read integer
        mov    [input], eax     ; store into input1

        mov    dx, 0
        mov    bx, 0
        mov    eax, 0
        mov    ax, [input]
        mov    bx, [value1]
        div    bx
        cmp    dx, 0
        je     fizz
        jne    not_fizz

        fizz:
        mov    eax, fizzValue
        call   print_string
        call   print_nl
        jmp    exit

        not_fizz:
        mov     ebx, [value2]
        cmp     eax, ebx
        je      buzz
        jne     normal

        buzz:
        mov    eax, buzzValue
        call   print_string
        call   print_nl
        jmp    exit

        normal:
        mov eax, 0
        mov   ax, [input]
        call  print_int
        call  print_nl

        exit:
        popa
        mov     eax, 0            ; return back to C
        leave
        ret

