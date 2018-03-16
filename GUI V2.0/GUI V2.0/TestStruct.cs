using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace GUI_V2._0
{
    /// <summary>
    /// Secondary class used for Thorlabs stage control
    /// </summary>
    public struct TestStruct
    {
        public UInt32 typeID;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 65)]
        public char[] description;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9)]
        public char[] serialNo;
        public UInt32 PID;
        [MarshalAs(UnmanagedType.I1)]
        public bool isKnownType;
        [MarshalAs(UnmanagedType.I1)]
        public bool _isMotor;
        [MarshalAs(UnmanagedType.I1)]
        public bool _isPiezzoMotor;
        [MarshalAs(UnmanagedType.I1)]
        public bool _isDCMotor;
        [MarshalAs(UnmanagedType.I1)]
        public bool _isStepperMotor;
        [MarshalAs(UnmanagedType.I1)]
        public bool isLaser;
        [MarshalAs(UnmanagedType.I1)]
        public bool isCustomType;
    }
}
