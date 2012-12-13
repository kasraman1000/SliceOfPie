﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using SliceOfPie;

namespace SliceOfPieServiceLibrary
{
    /**
     * This is where you can define all the operations the server should provide.
     * Add a new method to the interface and add the [OperationContract] attribute
     * to that method. The implementation of the method can be written in
     * SliceOfPieService.cs
     */
    [ServiceContract]
    public interface ISliceOfPieService
    {
        [OperationContract]
        List<Document> SyncAll(List<Document> docs);
    }
}