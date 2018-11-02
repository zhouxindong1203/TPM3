using System;

namespace TPM3.zxd.Helper
{
    public class DefaultText
    {
        /// <summary>
        /// 测试项的"充分性要求"
        /// </summary>
        /// <returns></returns>
        public static string GetItemSufficientDefaultText()
        {
            return string.Format("对测试项说明中列举的各功能或要求逐项进行测试。（功能）{0}" +
                                 "对测试项说明中列举的各性能指标逐项进行测试，测试应在不同使用条件下多次进行。（性能）{0}" +
                                 "对测试项说明中的各接口逐项进行测试，覆盖接口的正常和异常情况。（接口）", Environment.NewLine);
        }

        /// <summary>
        /// 测试项的"终止条件"
        /// </summary>
        /// <returns></returns>
        public static string GetItemTerminateDefaultText()
        {
            return string.Format("测试满足充分性要求或无法继续进行。（功能、接口）{0}" +
                                 "测试满足充分性要求或无法继续进行，记录实测结果并计算统计值（最大值、最小值、平均值等）。（性能）", Environment.NewLine);
        }

        /// <summary>
        /// 测试项的"评判标准"
        /// </summary>
        /// <returns></returns>
        public static string GetItemEvaluateDefaultText()
        {
            return string.Format("正确实现本测试项所覆盖的全部功能要求，功能的输入输出与约定一致，对异常输入或操作的边界、容错处理" +
                                 "等满足要求；（功能）{0}" +
                                 "正确实现本测试项所覆盖的全部性能要求，各性能指标的偏差（含随机抖动）或约束对软件运行造成的影响已被充分考虑" +
                                 "并处置；（性能）{0}" +
                                 "正确实现本测试项所覆盖的全部接口，接口协议、信息格式及数据内容等与约定一致，对异常输入的处理满足要求。（接口）", Environment.NewLine);
        }

        /// <summary>
        /// 测试项的"约束条件"
        /// </summary>
        /// <returns></returns>
        public static string GetItemConstrainDefaultText()
        {
            return string.Format("本测试项所覆盖内容（功能、性能、接口等）依据指定版本的软件需求规格说明等文档实现，测试环境" +
                                 "与软件运行环境（含运行约束）之间的差异不影响测试结果。");
        }

        /// <summary>
        /// 测试用例的"用例终止条件"
        /// </summary>
        /// <returns></returns>
        public static string GetCaseTerminateDefaultText()
        {
            return string.Format("本测试用例的全部测试步骤被执行（正常终止）；或者因故导致某测试步骤无法执行，测试用例" +
                                 "被迫中止（异常终止）。");
        }

        /// <summary>
        /// 测试用例的"用例通过准则"
        /// </summary>
        /// <returns></returns>
        public static string GetCasePassDefaultText()
        {
            return string.Format("本测试用例的全部测试步骤均通过（与其期望结果一致），标志本用例为“通过”；否则为不通过。");
        }
    }
}
