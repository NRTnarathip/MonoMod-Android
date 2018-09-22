﻿using System;
using System.Reflection;
using System.Linq.Expressions;
using MonoMod.Utils;
using System.Collections.Generic;
using Mono.Cecil;
using System.ComponentModel;
using Mono.Cecil.Cil;
using MethodBody = Mono.Cecil.Cil.MethodBody;
using System.Linq;

namespace MonoMod.RuntimeDetour.HookGen {
    /// <summary>
    /// Wrapper class used by the ILManipulator in HookExtensions.
    /// </summary>
    public class HookIL {

        public MethodBody Body { get; private set; }
        public ILProcessor IL { get; private set; }

        public MethodDefinition Method => Body.Method;
        public ModuleDefinition Module => Body.Method.Module;
        public Mono.Collections.Generic.Collection<Instruction> Instrs => Body.Instructions;

        internal HookIL(MethodBody body) {
            Body = body;
            IL = body.GetILProcessor();
        }

        public Instruction this[int index] {
            get {
                return Instrs[index];
            }
        }

        public HookILCursor this[Instruction instr] {
            get {
                return new HookILCursor(this, instr);
            }
        }

        public HookILCursor At(int index)
            => this[this[index]];

        public FieldReference Import(FieldInfo field)
            => Module.ImportReference(field);
        public MethodReference Import(MethodBase method)
            => Module.ImportReference(method);
        public TypeReference Import(Type type)
            => Module.ImportReference(type);

    }
}
