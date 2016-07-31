//-----------------------------------------------------------------------------------------------------------------------
// <copyright file="SpiResponse.cs" company="Lucio Benini">
//      Copyright (c) Lucio Benini. All rights reserved.
// </copyright>
// <author>Lucio Benini</author>
// <summary>Contains the SpiResponse enumerator.</summary>
// <remarks>
//      Iiriya's Serial Tester.
//      
//      This project contains a Serial Ports Tester.
//      
//      This solution and all its components its owned by Lucio Benini and his companies.
//      This solution is designed in C# for Microsoft .NET Framework and works only under the Windows Operative System.
//      For Mono or other platforms integrations contact the main project owner.
//      The designer and his companies aren't responsible for any damage due to unauthorized installations or usages.
//      
//      This project is created and designed under Microsoft .NET 3.5 environment and runtime and requires the runtime version 3.5 or later.
//      
//      This project is licensed. Copyright © Lucio Benini 2016. All Rights Reserved.
// </remarks>
//-----------------------------------------------------------------------------------------------------------------------

namespace Iiriya.Apps.SerialTester
{
    /// <summary>
    /// Indicates the response type.
    /// </summary>
    public enum SpiResponse
    {
        /// <summary>
        /// Text data.
        /// </summary>
        Text,

        /// <summary>
        /// Bytes.
        /// </summary>
        Bytes
    }
}