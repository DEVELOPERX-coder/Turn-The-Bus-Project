using SpiceSharp;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Ammeter : CircuitComponent
{
    public double Indicator = 0;
    public float Scale = 1.0e3f;

    public string componentTitleString = "";
    public string componentDescriptionString = "";

    public override void InitSpiceEntity(string name, string[] interfaces, float[] parameters,string title,string description)
    {
        this.name = name;
        this.Interfaces = interfaces;
        this.Parameters = parameters;
        this.Scale = parameters[0];
        this.Title = title;
        this.Description = description;

        spiceEntitys = new List<SpiceSharp.Entities.IEntity>();
        spiceEntitys.Add(new SpiceSharp.Components.Resistor(name, interfaces[0], interfaces[1], parameters[1]));
    }

    public override void RegisterComponent(Circuit circuit)
    {
        base.RegisterComponent(circuit);

        gameObject.GetComponentInChildren<AmmeterText>().InitAmmeterValue();
        var currentExport = new SpiceSharp.Simulations.RealPropertyExport(circuit.Sim, this.name, "i");
        circuit.Sim.ExportSimulationData += (sender, args) =>
        {
            this.Indicator = currentExport.Value;
            gameObject.GetComponentInChildren<AmmeterText>().UpdateAmmeterValue(this.Indicator * this.Scale);
        };
    }

    private void OnMouseDown()
    {
        System.Random random = new System.Random();
        float minValue = -0.05f;
        float maxValue = 0.05f;

        float randomFloat = (float)(random.NextDouble() * (maxValue - minValue) + minValue);

        Circuit.isLabelWindowOpen = true;
        Circuit.componentTitle = Title;
        Circuit.componentDescription = Description;
        if (this.Indicator * this.Scale >= 100 || this.Indicator * this.Scale <= -100)
        {
            Circuit.componentValue = string.Format("{0:0.##}", (this.Indicator * this.Scale / 1000)+randomFloat*(this.Indicator * this.Scale / 1000)) + " A";
        }
        else if (this.Indicator * this.Scale > 0.001 || this.Indicator * this.Scale < -0.001)
        {
            Circuit.componentValue = string.Format("{0:0.##}", (this.Indicator * this.Scale)+ randomFloat * (this.Indicator * this.Scale)) + " mA";
        }
        else
        {
            Circuit.componentValue = string.Format("{0:0.##}", (this.Indicator * this.Scale * 1e3) + randomFloat * (this.Indicator * this.Scale * 1e3)) + " \u03bcA";
        }

    }
}
