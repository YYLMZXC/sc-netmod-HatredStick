// 仇恨棍, 
// HatredStick.SubsystemHatredStickBlockBehavior
using Engine;
using Game;
using TemplatesDatabase;
namespace Game
{
    public class SubsystemHatredStickBlockBehavior : SubsystemBlockBehavior
    {
        public SubsystemAudio m_subsystemAudio;

        public SubsystemGameInfo m_subsystemGameInfo;

        public SubsystemFireBlockBehavior m_subsystemFireBlockBehavior;

        public SubsystemExplosivesBlockBehavior m_subsystemExplosivesBlockBehavior;

        public Game.Random m_random = new Game.Random();

        public ComponentChaseBehavior m_taget0;

        public ComponentChaseBehavior m_taget1;

        public override int[] HandledBlocks
        {
            get
            {
                return new int[1] { 942 };
            }
        }

        public override bool OnUse(Ray3 ray, ComponentMiner componentMiner)
        {
            object obj = componentMiner.Raycast(ray, RaycastMode.Digging);
            int num = Terrain.ExtractData(componentMiner.ActiveBlockValue);
            if (obj is BodyRaycastResult)
            {
                ComponentChaseBehavior componentChaseBehavior = ((BodyRaycastResult)obj).ComponentBody.Entity.FindComponent<ComponentChaseBehavior>();
                if (componentChaseBehavior != null)
                {
                    ComponentCreature componentCreature = componentChaseBehavior.Entity.FindComponent<ComponentCreature>();
                    if (num == 1)
                    {
                        m_taget1 = componentChaseBehavior;
                    }
                    else
                    {
                        m_taget0 = componentChaseBehavior;
                    }
                    componentMiner.ComponentPlayer.ComponentGui.DisplaySmallMessage(string.Format("目标{0}:{1}", ++num, componentCreature.DisplayName), Color.White, true, false);
                    return true;
                }
            }
            return false;
        }

        public override bool OnEditInventoryItem(IInventory inventory, int slotIndex, ComponentPlayer componentPlayer)
        {
            if (m_taget0 != m_taget1 && m_taget0 != null && m_taget1 != null)
            {
                ComponentCreature componentCreature = m_taget1.Entity.FindComponent<ComponentCreature>();
                ComponentCreature componentCreature2 = m_taget0.Entity.FindComponent<ComponentCreature>();
                m_taget0.Attack(componentCreature, 1000f, float.MaxValue, true);
                m_taget1.Attack(componentCreature2, 1000f, float.MaxValue, true);
                componentPlayer.ComponentGui.DisplaySmallMessage("仇恨展开 " + componentCreature2.DisplayName + " vs " + componentCreature.DisplayName, Color.White, true, false);
                return true;
            }
            return false;
        }

        public override void Load(ValuesDictionary valuesDictionary)
        {
            base.Load(valuesDictionary);
            m_subsystemAudio = base.Project.FindSubsystem<SubsystemAudio>(true);
            m_subsystemGameInfo = base.Project.FindSubsystem<SubsystemGameInfo>(true);
            m_subsystemFireBlockBehavior = base.Project.FindSubsystem<SubsystemFireBlockBehavior>(true);
            m_subsystemExplosivesBlockBehavior = base.Project.FindSubsystem<SubsystemExplosivesBlockBehavior>(true);
        }
    }
}
