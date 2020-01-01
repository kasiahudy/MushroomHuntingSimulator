﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEffect
{
    void Activate();

    void Deactivate();

    bool IsNegative();
}
