﻿using System.IO;
using UnityEngine;
/*
 * Basic class for writing data to a binary file.
 * if your data type isn't supported simply create a constructor for that data type
 */
public class GameDataWriter
{
    BinaryWriter writer;

    public GameDataWriter(BinaryWriter writer)
    {
        this.writer = writer;
    }

    public void Write(float value)
    {
        writer.Write(value);
    }

    public void Write(int value)
    {
        writer.Write(value);
    }

    public void Write(bool value)
    {
        writer.Write(value);
    }

    public void Write(Quaternion value)
    {
        writer.Write(value.x);
        writer.Write(value.y);
        writer.Write(value.z);
        writer.Write(value.w);
    }

    public void Write(Vector3 value)
    {
        writer.Write(value.x);
        writer.Write(value.y);
        writer.Write(value.z);
    }
}