﻿// Copyright (c) The Perspex Project. All rights reserved.
// Licensed under the MIT license. See licence.md file in the project root for full license information.

using System;
using System.Reactive.Linq;
using Xunit;

namespace Perspex.Base.UnitTests
{
    public class PerspexObjectTests_GetObservable
    {
        [Fact]
        public void GetObservable_Returns_Initial_Value()
        {
            Class1 target = new Class1();
            int raised = 0;

            target.GetObservable(Class1.FooProperty).Subscribe(x =>
            {
                if (x == "foodefault")
                {
                    ++raised;
                }
            });

            Assert.Equal(1, raised);
        }

        [Fact]
        public void GetObservable_Returns_Property_Change()
        {
            Class1 target = new Class1();
            bool raised = false;

            target.GetObservable(Class1.FooProperty).Subscribe(x => raised = x == "newvalue");
            raised = false;
            target.SetValue(Class1.FooProperty, "newvalue");

            Assert.True(raised);
        }

        [Fact]
        public void GetObservable_Returns_Property_Change_Only_For_Correct_Property()
        {
            Class2 target = new Class2();
            bool raised = false;

            target.GetObservable(Class1.FooProperty).Subscribe(x => raised = true);
            raised = false;
            target.SetValue(Class2.BarProperty, "newvalue");

            Assert.False(raised);
        }

        [Fact]
        public void GetObservable_Dispose_Stops_Property_Changes()
        {
            Class1 target = new Class1();
            bool raised = false;

            target.GetObservable(Class1.FooProperty)
                  .Subscribe(x => raised = true)
                  .Dispose();
            raised = false;
            target.SetValue(Class1.FooProperty, "newvalue");

            Assert.False(raised);
        }

        private class Class1 : PerspexObject
        {
            public static readonly PerspexProperty<string> FooProperty =
                PerspexProperty.Register<Class1, string>("Foo", "foodefault");
        }

        private class Class2 : Class1
        {
            public static readonly PerspexProperty<string> BarProperty =
                PerspexProperty.Register<Class2, string>("Bar", "bardefault");
        }
    }
}
