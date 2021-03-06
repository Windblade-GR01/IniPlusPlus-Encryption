import os
import sys
import struct


def main():
    key = sys.argv[3]
    f = open(sys.argv[1], "rb")
    data = ""
    data = f.read()
    f.close()
    out = convert(data, key)
    f2 = open(sys.argv[2], "wb")
    f2.write(out)
    f2.close()


def convert(data, key):
    v11 = range(256)
    v6 = [0] * 256
    v7 = 0
    if key:
        for i in xrange(256):
            if v7 == len(key):
                v7 = 0
            v6[i] = ord(key[v7])
            v7 += 1
    v7 = 0
    for i in xrange(256):
        v7 = (v6[i] + v11[i] + v7) % 256
        v10 = v11[i]
        v11[i] = v11[v7]
        v11[v7] = v10
    v7 = 0
    out = ''
    i = 0
    for j in xrange(len(data)):
        i = (i + 1) % 256
        v7 = (v7 + v11[i]) % 256
        v10 = v11[i]
        v11[i] = v11[v7]
        v11[v7] = v10
        v12 = (v11[v7] + v11[i]) % 256
        v5 = v11[v12]
        out += chr(ord(data[j]) ^ v5)
    return out


if __name__ == "__main__":
    main()
