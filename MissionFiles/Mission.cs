using System.IO;
using XMLParser.Elements;
namespace XMLParser.MissionFiles
{
    class Mission
    {
        private struct NodeStatus
        {
            public string name;
            public bool enabled;
        }
        readonly List<ElementBase> elements = new();
        readonly List<NodeStatus> nodes = new();
        public Mission(string heist_path, string continent_name)
        {
            string full_path = heist_path + "\\" + continent_name + "\\" + continent_name + ".mission";
            SharedClass.UpdateProgressText("Reading mission script", full_path);
            if (!File.Exists(full_path))
            {
                throw new MissionFileNotFoundException();
            }
            SharedClass.LoadDieselScript(out XmlNode table, full_path);
            if (table == null)
            {
                throw new MissionFileNotLoadedException();
            }
            foreach (XmlNode node in table.ChildNodes)
            {
                SharedClass.UpdateProgressText("Parsing mission script node: " + node.Name, full_path);
                nodes.Add(new NodeStatus()
                {
                    name = node.Name,
                    enabled = Convert.ToBoolean(node.Attributes["activate_on_parsed"].Value)
                });
                XmlNode elements = table.SelectSingleNode("/table/" + node.Name + "/elements");
                ParseElements(ref elements, node.Name);
            }
            EnsureUniqueElementNames();
            UpdateElementIDs();
        }

        private void ParseElements(ref XmlNode elements, string node)
        {
            SharedClass.UpdateProgress(0);
            SharedClass.SetProgressMax(elements.ChildNodes.Count);
            foreach (XmlNode element in elements.ChildNodes)
            {
                ReadElement(element, node);
                SharedClass.IncreaseProgress();
            }
        }

        private void ReadElement(XmlNode element, string node)
        {
            ElementBase e;
            string element_name = element.Attributes["class"].Value;
            string editor_name = element.Attributes["editor_name"].Value;
            int id = Convert.ToInt32(element.Attributes["id"].Value);
            XmlNodeList values = element.ChildNodes;
            switch (element_name)
            {
                case "ElementAIArea":
                    {
                        e = new ElementAIArea(element_name, editor_name, id, node, values);
                        break;
                    }
                case "ElementAIForceAttention":
                    {
                        e = new ElementAIForceAttention(element_name, editor_name, id, node, values);
                        break;
                    }
                case "ElementAreaDespawn":
                    {
                        e = new ElementAIArea(element_name, editor_name, id, node, values, "slots");
                        break;
                    }
                case "ElementAreaTrigger":
                    {
                        e = new ElementAreaTrigger(element_name, editor_name, id, node, values);
                        break;
                    }
                case "ElementDifficulty":
                    {
                        e = new ElementDifficulty(element_name, editor_name, id, node, values);
                        break;
                    }
                case "ElementUnitSequence":
                    {
                        e = new ElementUnitSequence(element_name, editor_name, id, node, values);
                        break;
                    }
                case "ElementUnitSequenceTrigger":
                    {
                        e = new ElementUnitSequenceTrigger(element_name, editor_name, id, node, values);
                        break;
                    }
                case "ElementEnableUnit":
                case "ElementDisableUnit":
                case "ElementLoadDelayed":
                case "ElementChangeVanSkin":
                case "ElementUnitDamage":
                    {
                        e = new ElementEnableUnit(element_name, editor_name, id, node, values);
                        break;
                    }
                case "ElementCheckDLC":
                    {
                        e = new ElementCheckDLC(element_name, editor_name, id, node, values);
                        break;
                    }
                case "ElementObjective":
                case "ElementCounterOperator":
                    {
                        e = new ElementInstanceSetParams(element_name, editor_name, id, node, values, "instance_var_names");
                        break;
                    }
                case "ElementCounter":
                    {
                        e = new ElementCounter(element_name, editor_name, id, node, values);
                        break;
                    }
                case "ElementStopwatch":
                    {
                        e = new ElementStopwatch(element_name, editor_name, id, node, values);
                        break;
                    }
                case "ElementStopwatchFilter":
                    {
                        e = new ElementStopwatchFilter(element_name, editor_name, id, node, values);
                        break;
                    }
                case "ElementTimer":
                    {
                        e = new ElementTimer(element_name, editor_name, id, node, values);
                        break;
                    }
                case "ElementInstanceInputEvent":
                case "ElementInstanceOutputEvent":
                    {
                        e = new ElementInstance(element_name, editor_name, id, node, values);
                        break;
                    }
                case "ElementRandomInstanceInputEvent":
                    {
                        e = new ElementInstance(element_name, editor_name, id, node, values, "instances");
                        break;
                    }
                case "ElementInstancePoint":
                    {
                        e = new ElementInstancePoint(element_name, editor_name, id, node, values);
                        break;
                    }
                case "ElementMissionFilter":
                    {
                        e = new ElementMissionFilter(element_name, editor_name, id, node, values);
                        break;
                    }
                case "ElementAIGraph":
                    {
                        e = new ElementAIGraph(element_name, editor_name, id, node, values);
                        break;
                    }
                case "ElementSpecialObjective":
                case "ElementSpecialObjectiveGroup":
                    {
                        e = new ElementSpecialObjective(element_name, editor_name, id, node, values);
                        break;
                    }
                case "ElementEnemyPreferedAdd":
                    {
                        e = new ElementEnemyPreferedAdd(element_name, editor_name, id, node, values);
                        break;
                    }
                case "ElementSpawnEnemyGroup":
                    {
                        e = new ElementSpawnEnemyGroup(element_name, editor_name, id, node, values);
                        break;
                    }
                case "ElementPrePlanning":
                    {
                        e = new ElementPrePlanning(element_name, editor_name, id, node, values);
                        break;
                    }
                case "ElementPrePlanningExecuteGroup":
                    {
                        e = new ElementPrePlanningExecuteGroup(element_name, editor_name, id, node, values);
                        break;
                    }
                case "ElementSpawnCivilian":
                case "ElementSpawnGageAssignment":
                case "ElementSpawnEnemyDummy":
                    {
                        e = new ElementSpecialObjective(element_name, editor_name, id, node, values, "orientation_elements");
                        break;
                    }
                case "ElementAlertTrigger":
                    {
                        e = new ElementSpawnEnemyGroup(element_name, editor_name, id, node, values, "alert_types");
                        break;
                    }
                case "ElementLaserTrigger":
                    {
                        e = new ElementLaserTrigger(element_name, editor_name, id, node, values);
                        break;
                    }
                case "ElementNavObstacle":
                    {
                        e = new ElementNavObstacle(element_name, editor_name, id, node, values);
                        break;
                    }
                case "ElementHeistTimer":
                    {
                        e = new ElementHeistTimer(element_name, editor_name, id, node, values);
                        break;
                    }
                case "ElementTimerOperator":
                    {
                        e = new ElementTimerOperator(element_name, editor_name, id, node, values);
                        break;
                    }
                case "ElementAreaReportTrigger":
                    {
                        e = new ElementAreaReportTrigger(element_name, editor_name, id, node, values);
                        break;
                    }
                case "ElementInstigatorRule":
                    {
                        e = new ElementInstigatorRule(element_name, editor_name, id, node, values);
                        break;
                    }
                case "ElementRandomInstance":
                    {
                        e = new ElementRandomInstance(element_name, editor_name, id, node, values);
                        break;
                    }
                case "ElementRandomInstanceInput":
                    {
                        e = new ElementRandomInstanceInput(element_name, editor_name, id, node, values);
                        break;
                    }
                case "ElementRandomInstanceOutput":
                    {
                        e = new ElementRandomInstanceOutput(element_name, editor_name, id, node, values);
                        break;
                    }
                case "ElementRandom":
                    {
                        e = new ElementRandom(element_name, editor_name, id, node, values);
                        break;
                    }
                case "ElementInstanceParams":
                    {
                        e = new ElementInstanceParams(element_name, editor_name, id, node, values);
                        break;
                    }
                case "ElementInstanceSetParams":
                    {
                        e = new ElementInstanceSetParams(element_name, editor_name, id, node, values);
                        break;
                    }
                case "ElementEnableSoundEnvironment":
                    {
                        e = new ElementEnableSoundEnvironment(element_name, editor_name, id, node, values);
                        break;
                    }
                case "ElementVehicleBoarding":
                    {
                        e = new ElementVehicleBoarding(element_name, editor_name, id, node, values);
                        break;
                    }
                case "ElementClock":
                    {
                        e = new ElementClock(element_name, editor_name, id, node, values);
                        break;
                    }
                default:
                    {
                        e = new ElementBase(element_name, editor_name, id, node, values);
                        break;
                    }
            }
            this.elements.Add(e);
        }

        private void EnsureUniqueElementNames()
        {
            //Used memory will suck, but atleast it will work
            List<string> names = new();
            List<string> new_names = new();
            List<int> changed_elements = new();
            foreach (ElementBase element in this.elements)
            {
                string element_name = element.GetEditorNamePure();
                if (names.Contains(element_name))
                {
                    int i = 2;
                    while (true)
                    {
                        string new_name = element_name + "_" + i.ToString();
                        if (names.Contains(new_name))
                        {
                            i++;
                        }
                        else
                        {
                            names.Add(new_name);
                            new_names.Add(new_name);
                            changed_elements.Add(element.GetElementID());
                            break;
                        }
                    }
                }
                else
                {
                    names.Add(element_name);
                }
            }
            for (int i = 0; i < changed_elements.Count; i++)
            {
                int element_id = changed_elements[i];
                foreach (ElementBase element in this.elements)
                {
                    if (element.GetElementID() == element_id)
                    {
                        element.UpdateEditorName(new_names[i]);
                    }
                }
            }
        }

        private void UpdateElementIDs()
        {
            foreach (ElementBase element in this.elements)
            {
                int id = element.GetElementID();
                string editor_name = element.GetEditorName();
                bool disabled = element.IsElementDisabled();
                foreach (ElementBase e in this.elements)
                {
                    e.UpdateElements(id, editor_name, disabled);
                }
            }
        }

        public void UpdateInstancesInElements(string instance_name, string instance_formatted_name)
        {
            foreach (ElementBase element in this.elements)
            {
                if (element.HasInstances())
                {
                    element.UpdateInstances(instance_name, instance_formatted_name);
                }
            }
        }

        public void UpdateUnitsInElements(int unit_id, string unit_name, string unit_pos)
        {
            foreach (ElementBase element in this.elements)
            {
                if (element.HasUnits())
                {
                    element.UpdateUnitElements(null, unit_id, unit_name, unit_pos);
                }
            }
        }

        public void Save(string save_path)
        {
            CheckForErrors();
            foreach (NodeStatus node in this.nodes)
            {
                using FileStream fs = new(save_path + " " + node.name + " " + node.enabled.ToString() + ".txt", FileMode.Create, FileAccess.Write);
                using StreamWriter sw = new(fs);
                SaveElements(sw, node.name);
            }
        }

        private void SaveElements(StreamWriter sw, string node)
        {
            foreach (ElementBase element in this.elements)
            {
                if (element.GetNodeName() == node)
                {
                    sw.Write(element.ToString());
                }
            }
        }

        private void CheckForErrors()
        {
            foreach (ElementBase element in this.elements)
            {
                if (!element.AllChildsLoaded())
                {
                    SharedClass.AddError(element);
                }
            }
        }
    }

    class MissionFileNotFoundException : Exception
    {
        public MissionFileNotFoundException()
        {
        }
    }

    class MissionFileNotLoadedException : Exception
    {
        public MissionFileNotLoadedException()
        {
        }
    }
}