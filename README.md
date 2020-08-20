# KTLCD3Reset

A small tool to reset the KT-LCD3 without a second display, useful if you're locked out of the display or just need to reset it. I wrote this because i lost my password and was unable to get back into my display.

The tool does only one thing, spam a byte sequence over UART (9600 8 n 1), i recorded this byte sequence while C11 was set to 12.

```c#
{0xFF,0x1C,0x19,0x00,0xA0,0x00,0x00,0x01,0x0B,0x02,0x00,0x00,0x00,0x0A,0x05,0x00,0x00,0x00,0x14,0x04,0x05,0x02,0x11}
```

These values represent the folowing data:

255 (Reset Instruction?), Wheel Diameter (Inches), Max Speed (Km/h), PAS Level, P1, P2, P3, P4, P5, C1, C2, C3, C4, C5, C6, C7, C8, C9, C10, C12, C13, C14, CRC Checksum?

Downloads: https://github.com/Jnnshschl/KTLCD3Reset/releases

## How to use the Tool

### Prerequisites

Things you need:

* .Net Core 3.1
* USB to UART Converter
* KT LCD 3
* Jumper Wires

![usb-to-serial](https://github.com/Jnnshschl/KTLCD3Reset/raw/master/img/usb-to-serial.png)

### Wiring setup

* USB Red       => None
* KTLCD3 Blue   => None
* KTLCD3 Red    => Battery + Positive
* KTLCD3 Black  => Battery - Negative
* USB Black     => Battery Negative
* USB Green     => KTLCD3 Green
* USB Yellow    => KTLCD3 Yellow

![wiring](https://github.com/Jnnshschl/KTLCD3Reset/raw/master/img/wiring.png)

### Running the Tool

1. Start the Tool (replace COM8 with the port of your USB-to-UART converter)

```
> KTLCD3Reset-x64.exe COM8
```

2. Turn on the Display
3. Within 5s hold both UP and DOWN buttons for 2s
4. Profit

The display should look like this if everything worked, if not try it again.

![kt-lcd-3](https://github.com/Jnnshschl/KTLCD3Reset/raw/master/img/kt-lcd-3.jpg)
