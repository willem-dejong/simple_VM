AND R5 R3 -12
AND R0 R1 5
ADD R3 R2 R6
NOT R4 R5
BR z x5
AND R2 R2 0
AND R3 R3 0
ADD R3 R3 15
ADD R3 R3 2
ADD R2 R2 15
ADD R3 R3 -1
BR p x0009
LD R0 x0
AND R0 R0 R2
ST R0 x101
LD R0 x1
AND R0 R0 R2
ST R0 x102
LD R0 x2
AND R0 R0 R2
ST R0 x103
LD R0 x3
AND R0 R0 R2
ST R0 x104
AND R2 R2 0
ADD R2 R2 -1
ST R2 x105
TRAP x25